namespace MarkTek.Fluent.Testing.RecordGeneration
{
    public interface IRecordService<TID>
    {
        void AssertAgainst<TSpec>(TSpec Specifications) where TSpec : ISpecifcation;
        void Cleanup();
        RecordService<TID> CreateRecord<T>(IRecordCreator<T, TID> app);
        RecordService<TID> CreateRelatedRecord<T>(IRelatedRecordCreator<T, TID> app);
    }
}