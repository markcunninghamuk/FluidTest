namespace MarkTek.Fluent.Testing.RecordGeneration
{
    public interface IRecordCreator<TEntity,TID>
    {
        Record<TEntity, TID> CreateRecord();
    }
}
