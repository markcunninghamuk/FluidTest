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
        /// Uses a class that implements ISpecification to check the output of the record creation
        /// </summary>
        /// <typeparam name="TSpec"></typeparam>
        /// <param name="Specifications"></param>
        IRecordService<TID> AssertAgainst<TSpec>(List<TSpec> Specifications) where TSpec : ISpecifcation;
        
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