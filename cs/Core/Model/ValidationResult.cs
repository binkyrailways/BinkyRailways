namespace BinkyRailways.Core.Model
{
    /// <summary>
    /// Single error/warning resulting from a validation request.
    /// </summary>
    public class ValidationResult
    {
        private readonly IEntity entity;
        private readonly string message;

        /// <summary>
        /// Default ctor
        /// </summary>
        public ValidationResult(IEntity entity, string message)
        {
            this.entity = entity;
            this.message = message;
        }

        /// <summary>
        /// Human readable error/warning message
        /// </summary>
        public string Message
        {
            get { return message; }
        }

        /// <summary>
        /// Entity this result is about.
        /// </summary>
        public IEntity Entity
        {
            get { return entity; }
        }
    }
}
