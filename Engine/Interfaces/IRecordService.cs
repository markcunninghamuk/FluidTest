using Marktek.Fluent.Testing.Engine;
using Marktek.Fluent.Testing.Engine.Interfaces;
using System;
using System.Collections.Generic;

namespace MarkTek.Fluent.Testing.RecordGeneration
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TID"></typeparam>
    public interface IRecordService<TID>
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="spec"></param>
        /// <returns></returns>
        IRecordService<TID> AssertAgainst<TType>(BaseValidator<TID, TType> spec);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="milliseconds"></param>
        /// <returns></returns>
        IRecordService<TID> Delay(int milliseconds);

        /// <summary>
        /// Creates a record of type T where T is a class
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="implementation"></param>
        /// <returns></returns>
        IRecordService<TID> CreateRecord<T>(IRecordCreator<T, TID> implementation);

        /// <summary>
        /// Creates a related record of type T where T is a class and passes in the previously created id from the CreateRecord method
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="implementation"></param>
        /// <returns></returns>
        IRecordService<TID> CreateRelatedRecord<T>(IRelatedRecordCreator<T, TID> implementation);

        /// <summary>
        /// Creates a related record of type T where T is a class and passes in the previously created id from the CreateRecord method
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="implementation"></param>
        /// <returns></returns>
        IRecordService<TID> ExecuteAction<T>(IExecutableAction<T,TID> implementation);

        /// <summary>
        /// Creates a related record of type T where T is a class and passes in the previously created id from the CreateRecord method
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="implementation"></param>
        /// <returns></returns>
        IRecordService<TID> ExecuteActionOnAggregate<T>(IExecutableAggregateAction<T,TID> implementation);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cond"></param>
        /// <param name="builder"></param>
        /// <returns></returns>
        IRecordService<TID> If(bool cond, Func<IRecordService<TID>, IRecordService<TID>> builder);

         /// <summary>
         /// An identifier used to cleanup, Up to the developer to decide how best to do this. Consider using a sessionId fr the test run
         /// </summary>
         /// <param name="cleanup"></param>
         /// <param name="aggregateId"></param>
        void Cleanup(IRecordCleanup<TID> cleanup);
               
    }
}