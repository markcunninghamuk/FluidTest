namespace MarkTek.Fluent.Testing.RecordGeneration
{
    /// <summary>
    /// Creates a related record, this method should be called after the CreateRecordMethod on IRecordCreator, it passes in the parent object of the previous call
    /// </summary>
    /// <typeparam name="TParent"></typeparam>
    /// <typeparam name="TID"></typeparam>
    /// <typeparam name="TID"></typeparam>
    public interface IRelatedRecordCreator<TParent,TEntity, TID>
    {
        /// <summary>
        /// Creates a record of Type T by passing in the previous id of the IRecordCreator.CreateRecord method
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Record<TEntity, TID> CreateRecord(TParent entity);
    }
}