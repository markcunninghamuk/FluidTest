namespace MarkTek.Fluent.Testing.RecordGeneration
{
    /// <summary>
    /// An interface that should be implemented to ensure the tests meet the expected criteria
    /// </summary>
    public interface ISpecifcation
    {
        /// <summary>
        /// Validates against a set of created records, scenarios
        /// </summary>
        void Validate();
    }
}
