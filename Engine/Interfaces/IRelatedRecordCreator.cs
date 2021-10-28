using System.Collections.Generic;

namespace MarkTek.Fluent.Testing.RecordGeneration
{
    /// <summary>
    /// Creates a related record, this method should be called after the CreateRecordMethod on IRecordCreator
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TID"></typeparam>
    public interface IRelatedRecordCreator<TEntity, TID>
    {
        /// <summary>
        /// Creates a record of Type T by passing in the previous id of the IRecordCreator.CreateRecord method
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Record<TEntity, TID> CreateRecord(TID id);
    }

    /// <summary>
    /// Creates a related record, this method should be called after the CreateRecordMethod on IRecordCreator
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TID"></typeparam>
    public interface IRelatedRecordCreatorComposite<TEntity, TID>
    {
        /// <summary>
        /// Creates a record of Type T by passing in the previous id of the IRecordCreator.CreateRecord method
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Record<TEntity, TID> CreateRecord(List<Record<object,TID>> id);
    }

}