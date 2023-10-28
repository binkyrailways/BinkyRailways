using System;
using BinkyRailways.Core.Util;

namespace BinkyRailways.Core.State.Impl
{
    /// <summary>
    /// Value of a property in a state object.
    /// This implementation only has 1 value, so requested and actual value are the same.
    /// </summary>
    internal class SimpleStateProperty<T> : IStateProperty<T>, IDisposable
    {
        /// <summary>
        /// Fired when the requested value has changed.
        /// </summary>
        public event EventHandler RequestedChanged;

        /// <summary>
        /// Fired when the actual value has changed.
        /// </summary>
        public event EventHandler ActualChanged;

        private readonly Func<T, T> validate;
        private readonly Action<T> valueChanged;
        private T actual;

        /// <summary>
        /// Default ctor
        /// </summary>
        public SimpleStateProperty(T initialValue, Func<T, T> validate, Action<T> valueChanged)
        {
            this.validate = validate;
            this.valueChanged = valueChanged;
            actual = initialValue;
        }

        /// <summary>
        /// Gets / sets the requested value
        /// </summary>
        public T Requested
        {
            get { return actual; }
            set { Actual = value; }
        }

        /// <summary>
        /// Gets / sets the actual value
        /// </summary>
        public T Actual
        {
            get { return actual; }
            set
            {
                if (validate != null)
                {
                    value = validate(value);
                }
                if (!Equals(actual, value))
                {
                    actual = value;
                    if (valueChanged != null)
                    {
                        valueChanged(value);
                    }
                    RequestedChanged.Fire(this);
                    ActualChanged.Fire(this);
                }
            }
        }

        /// <summary>
        /// Is the request value equal to the actual value?
        /// </summary>
        public bool IsConsistent { get { return true; } }

        /// <summary>
        /// Cleanup
        /// </summary>
        public void Dispose()
        {
            RequestedChanged = null;
            ActualChanged = null;
        }

        /// <summary>
        /// Convert to string
        /// </summary>
        public override string ToString()
        {
            return string.Format("{0}", actual);
        }
    }
}
