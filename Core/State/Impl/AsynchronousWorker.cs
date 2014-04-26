using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using BinkyRailways.Core.Util;
using NLog;

namespace BinkyRailways.Core.State.Impl
{
    /// <summary>
    /// Threaded worker that performs actions in sequence on a different thread.
    /// </summary>
    public class AsynchronousWorker : IDisposable
    {
        private event EventHandler Stop;

        private readonly string threadName;
        private readonly List<Action> actions = new List<Action>();
        private readonly List<DelayedAction> delayedActions = new List<DelayedAction>();
        private readonly object actionsLock = new object();
        private bool started;
        private bool stop;
        private Thread thread;
        private readonly Logger log;

        /// <summary>
        /// Default ctor
        /// </summary>
        internal AsynchronousWorker(string threadName)
        {
            this.threadName = threadName;
            log = LogManager.GetLogger(threadName);
        }

        /// <summary>
        /// Are there no immediate actions waiting?
        /// </summary>
        public bool Idle
        {
            get
            {
                lock (actionsLock)
                {
                    return (actions.Count == 0);
                }
            }
        }

        /// <summary>
        /// Post the given action on the dispatch action list.
        /// The dispatcher will then perform the action on the dispatch thread.
        /// </summary>
        public void PostAction(Action action)
        {
            lock (actionsLock)
            {
                actions.Add(action);
                Monitor.PulseAll(actionsLock);
                if (!started)
                {
                    StartDispatchThread();
                }
            }
        }

        /// <summary>
        /// Post the given delayed action on the dispatch action list.
        /// The action will be performed at the given time.
        /// </summary>
        public void PostDelayedAction(DateTime time, Action action)
        {
            lock (actionsLock)
            {
                delayedActions.Add(new DelayedAction(action, time));
                delayedActions.Sort();
                Monitor.PulseAll(actionsLock);
                if (!started)
                {
                    StartDispatchThread();
                }
            }
        }

        /// <summary>
        /// Post the given delayed action on the dispatch action list.
        /// The action will be performed after given timeout.
        /// </summary>
        public void PostDelayedAction(TimeSpan timeout, Action action)
        {
            PostDelayedAction(DateTime.Now.Add(timeout), action);
        }

        /// <summary>
        /// Start the dispatcher thread.
        /// </summary>
        private void StartDispatchThread()
        {
            thread = new Thread(Run);
            thread.Name = threadName;
            thread.Start();
            started = true;
        }

        /// <summary>
        /// Runner of the dispatch thread.
        /// </summary>
        private void Run()
        {
            while (!stop)
            {
                var now = DateTime.Now;
                Action action = null;
                lock (actionsLock)
                {
                    // Any delayed actions that should be run?
                    var firstDelayedAction = (delayedActions.Count > 0) ? delayedActions[0] : null;
                    if ((firstDelayedAction != null) && (firstDelayedAction.Time <= now))
                    {
                        action = delayedActions[0].Action;
                        delayedActions.RemoveAt(0);
                    }
                    else if (actions.Count > 0)
                    {
                        action = actions[0];
                        actions.RemoveAt(0);
                    }
                    else
                    {
                        // Wait a while.
                        var timeout = (firstDelayedAction != null)
                                          ? firstDelayedAction.Time.Subtract(now)
                                          : new TimeSpan(0, 1, 0, 0);
                        Monitor.Wait(actionsLock, timeout);
                    }
                }
                if (action != null)
                {
                    // Perform action
                    try
                    {
                        action();
                    }
                    catch (Exception ex)
                    {
                        log.LogException(LogLevel.Error, Strings.FailedToPerformAction, ex);
                    }
                }
            }
            thread = null;
        }

        /// <summary>
        /// Cleanup and stop the dispatch thread.
        /// </summary>
        public void Dispose()
        {
            lock (actionsLock)
            {
                stop = true;
                Monitor.PulseAll(actionsLock);
                Stop.Fire(this);
            }
        }

        private class DelayedAction : IComparable<DelayedAction>
        {
            private readonly Action action;
            private readonly DateTime time;

            /// <summary>
            /// Default ctor
            /// </summary>
            public DelayedAction(Action action, DateTime time)
            {
                this.action = action;
                this.time = time;
            }

            /// <summary>
            /// Time when the action will be run.
            /// </summary>
            public DateTime Time
            {
                get { return time; }
            }

            /// <summary>
            /// The action to perform
            /// </summary>
            public Action Action
            {
                get { return action; }
            }

            /// <summary>
            /// Compares the current object with another object of the same type.
            /// </summary>
            public int CompareTo(DelayedAction other)
            {
                return time.CompareTo(other.time);
            }
        }

        /// <summary>
        /// Post the given action on the dispatcher thread and wait for the result.
        /// </summary>
        public T PostActionAndWait<T>(Func<T> action)
        {
            // Are we on the worker thread?
            if (Thread.CurrentThread == thread)
            {
                // Run right now
                return action();
            }

            // Do actual post and wait
            using (var helper = new PostActionAndWaitHelper<T>(action, this))
            {
                PostAction(helper.Run);
                return helper.Result();
            }
        }

        /// <summary>
        /// Helper class for PostActionAndWait
        /// </summary>
        private class PostActionAndWaitHelper<T> : IDisposable
        {
            private readonly AsynchronousWorker worker;
            private T result;
            private bool resultAvailable;
            private readonly Func<T> action;

            /// <summary>
            /// Default ctor
            /// </summary>
            public PostActionAndWaitHelper(Func<T> action, AsynchronousWorker worker)
            {
                this.action = action;
                this.worker = worker;
                worker.Stop += OnWorkerStop;
            }

            /// <summary>
            /// Worker is being stopped.
            /// </summary>
            void OnWorkerStop(object sender, EventArgs e)
            {
                lock (this)
                {
                    Monitor.PulseAll(this);
                }
            }

            /// <summary>
            /// Disconnect
            /// </summary>
            void IDisposable.Dispose()
            {
                worker.Stop -= OnWorkerStop;                
            }

            /// <summary>
            /// Run the action
            /// </summary>
            public void Run()
            {
                var localResult = action();
                lock (this)
                {
                    result = localResult;
                    resultAvailable = true;
                    Monitor.PulseAll(this);
                }
            }

            /// <summary>
            /// Wait for the result and return it.
            /// </summary>
            public T Result()
            {
                lock (this)
                {
                    while (!resultAvailable && !worker.stop)
                    {
                        Monitor.Wait(this);
                    }
                    return result;
                }
            }
        }
    }
}
