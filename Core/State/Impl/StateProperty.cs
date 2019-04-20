using System;
using System.ComponentModel;
using BinkyRailways.Core.Util;

namespace BinkyRailways.Core.State.Impl
{
    /// <summary>
    /// Value of a property in a state object.
    /// The value contains a requested value and an actual value.
    /// </summary>
    [TypeConverter(typeof(ExpandableObjectConverter))]
    internal class StateProperty<T> : IStateProperty<T>, IDisposable
    {
        /// <summary>
        /// Fired when the requested value has changed.
        /// </summary>
        public event EventHandler RequestedChanged;

        /// <summary>
        /// Fired when the actual value has changed.
        /// </summary>
        public event EventHandler ActualChanged;

        private readonly EntityState owner;
        private T requested;
        private readonly Func<T, T> validate;
        private readonly Action<T> requestedValueChanged;
        private readonly Action<T> actualValueChanged;
        private T actual;
        private readonly bool cacheRequested;

        /// <summary>
        /// Default ctor
        /// </summary>
        public StateProperty(EntityState owner, T initialValue, Func<T, T> validate,
            Action<T> requestedValueChanged, Action<T> actualValueChanged) :
            this(owner, initialValue, validate, requestedValueChanged, actualValueChanged, true)
        {            
        }

        /// <summary>
        /// Default ctor
        /// </summary>
        public StateProperty(EntityState owner, T initialValue, Func<T, T> validate,
            Action<T> requestedValueChanged, Action<T> actualValueChanged, bool cacheRequested)
        {
            this.owner = owner;
            requested = initialValue;
            this.validate = validate;
            this.requestedValueChanged = requestedValueChanged;
            this.actualValueChanged = actualValueChanged;
            this.cacheRequested = cacheRequested;
            actual = initialValue;
            FirstRequest = true;
        }

        /// <summary>
        /// Gets / sets the requested value
        /// </summary>
        [DisplayName(@"Requested")]
        public T Requested
        {
            get { return requested; }
            set
            {
                if (validate != null)
                {
                    value = validate(value);
                }
                if ((!cacheRequested) || (!Equals(requested, value)) || FirstRequest)
                {
                    requested = value;
                    FirstRequest = false;
                    if (requestedValueChanged != null)
                    {
                        requestedValueChanged(value);
                    }
                    RequestedChanged.Fire(this);
                    if (owner != null)
                    {
                        owner.OnRequestedStateChanged();
                    }
                }
            }
        }

        /// <summary>
        /// Gets / sets the actual value
        /// </summary>
        [ReadOnly(true)]
        [DisplayName(@"Actual")]
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
                    if (actualValueChanged != null)
                    {
                        actualValueChanged(value);
                    }
                    ActualChanged.Fire(this);
                    if (owner != null)
                    {
                        owner.OnActualStateChanged();
                    }
                }
            }
        }

        /// <summary>
        /// If true, all values set to <see cref="Requested"/> property will trigger
        /// an changed action. The property is automatically reset when the
        /// <see cref="Requested"/> is set.
        /// </summary>
        [ReadOnly(true)]
        [DisplayName(@"First request")]
        public bool FirstRequest { get; set; }

        /// <summary>
        /// Is the actual value equal to the requested value?
        /// </summary>
        [DisplayName(@"Consistent")]
        public bool IsConsistent
        {
            get { return Equals(actual, requested); }
        }

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
            return string.Format("{0}/{1}", requested, actual);
        }
    }
}
