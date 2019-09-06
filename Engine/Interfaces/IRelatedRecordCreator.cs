namespace MarkTek.Fluent.Testing.RecordGeneration
{
    public interface IRelatedRecordCreator<TEntity, TID>
    {
        Record<TEntity, TID> CreateRecord(TID id);
    }
}
