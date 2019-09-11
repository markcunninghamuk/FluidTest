namespace MarkTek.Fluent.Testing.RecordGeneration
{
/// <summary>
/// Test
/// </summary>
/// <typeparam name="TEntity"></typeparam>
/// <typeparam name="TID"></typeparam>
    public interface IRecordCreator<TEntity,TID>
    {
        /// <summary>
        /// Creates a parent / aggregate record that children may be linked to.
        /// For No-SQL stores this will be used and CreateRelated would technically not be required
        /// </summary>
        /// <returns></returns>
        Record<TEntity, TID> CreateRecord();
    }
}
