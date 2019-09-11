namespace MarkTek.Fluent.Testing.RecordGeneration
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TID"></typeparam>
    public interface IRecordService<TID>
    {
        /// <summary>
        /// Uses a class that implements ISpecification to check the output of the record creation
        /// </summary>
        /// <typeparam name="TSpec"></typeparam>
        /// <param name="Specifications"></param>
        void AssertAgainst<TSpec>(TSpec Specifications) where TSpec : ISpecifcation;

        /// <summary>
        /// Creates a record of type T where T is a class
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="implementation"></param>
        /// <returns></returns>
        RecordService<TID> CreateRecord<T>(IRecordCreator<T, TID> implementation);

        /// <summary>
        /// Creates a related record of type T where T is a class and passes in the previously created id from the CreateRecord method
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="implementation"></param>
        /// <returns></returns>
        RecordService<TID> CreateRelatedRecord<T>(IRelatedRecordCreator<T, TID> implementation);
    }
}