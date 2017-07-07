using System;
using System.ComponentModel;
using BinkyRailways.Core.Util;
using Newtonsoft.Json;

namespace BinkyRailways.Core.State.Impl
{
    /// <summary>
    /// Value of an actual property in a state object.
    /// </summary>
    [TypeConverter(typeof(ExpandableObjectConverter))]
    [JsonObject]
    public class ActualStateProperty<T> : IActualStateProperty<T>, IDisposable
    {
        /// <summary>
        /// Fired when the actual value has changed.
        /// </summary>
        public event EventHandler ActualChanged;

        private readonly EntityState owner;
        private readonly Func<T, T> validate;
        private readonly Action<T> valueChanged;
        private T actual;

        /// <summary>
        /// Default ctor
        /// </summary>
        public ActualStateProperty(EntityState owner, T initialValue, Func<T, T> validate, Action<T> valueChanged)
        {
            this.owner = owner;
            this.validate = validate;
            this.valueChanged = valueChanged;
            actual = initialValue;
        }

        /// <summary>
        /// Gets / sets the actual value
        /// </summary>
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
                    if (valueChanged != null)
                    {
                        valueChanged(value);
                    }
                    ActualChanged.Fire(this);
                    owner.OnActualStateChanged();
                }
            }
        }

        /// <summary>
        /// Cleanup
        /// </summary>
        public void Dispose()
        {
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
