namespace BinkyRailways.Core.Model
{
    /// <summary>
    /// Interface implemented by types that can be validated.
    /// </summary>
    public interface IValidationSubject
    {
        /// <summary>
        /// Validate the integrity of this entity.
        /// </summary>
        void Validate(ValidationResults results);
    }
}
