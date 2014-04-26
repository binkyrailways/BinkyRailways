namespace BinkyRailways.Core.Model.Impl
{
    /// <summary>
    /// Property value holder.
    /// </summary>
    internal class Property<T>
    {
        private readonly IModifiable entity;
        private T value;
        private readonly System.Action<T> valueChanged;

        /// <summary>
        /// Default ctor
        /// </summary>
        internal Property(IModifiable entity, T value)
            : this(entity, value, null)
        {
        }

        /// <summary>
        /// Default ctor
        /// </summary>
        internal Property(IModifiable entity, T value, System.Action<T> valueChanged)
        {
            this.entity = entity;
            this.value = value;
            this.valueChanged = valueChanged;
        }

        /// <summary>
        /// Actual value
        /// </summary>
        public T Value
        {
            get { return value; }
            set
            {
                if (!Equals(value, this.value))
                {
                    T oldValue = this.value;
                    this.value = value;
                    if(valueChanged != null)
                    {
                        valueChanged(value);
                    }
                    entity.OnModified();
                }
            }
        }
    }
}
