using System;
using System.Collections.Generic;

namespace BinkyRailways.Core.Model
{
    /// <summary>
    /// Results from a validation request.
    /// </summary>
    public class ValidationResults
    {
        private readonly List<ValidationResult> errors = new List<ValidationResult>();
        private readonly List<ValidationResult> warnings = new List<ValidationResult>();

        /// <summary>
        /// Gets the number of validation errors.
        /// </summary>
        public int ErrorCount { get { return errors.Count; } }

        /// <summary>
        /// Gets the number of validation warnings.
        /// </summary>
        public int WarningCount { get { return warnings.Count; } }

        /// <summary>
        /// Gets all validation errors.
        /// </summary>
        public IEnumerable<ValidationResult> Errors { get { return errors; } }

        /// <summary>
        /// Gets all validation warnings.
        /// </summary>
        public IEnumerable<ValidationResult> Warnings { get { return warnings; } }

        /// <summary>
        /// Add an error
        /// </summary>
        internal void Error(IEntity entity, string message, params object[] args)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            if ((args != null) && (args.Length > 0))
            {
                message = string.Format(message, args);
            }
            errors.Add(new ValidationResult(entity, message));
        }

        /// <summary>
        /// Add an warning
        /// </summary>
        internal void Warn(IEntity entity, string message, params object[] args)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            if ((args != null) && (args.Length > 0))
            {
                message = string.Format(message, args);
            }
            warnings.Add(new ValidationResult(entity, message));
        }
    }
}
