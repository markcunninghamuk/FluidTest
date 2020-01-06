using Marktek.Fluent.Testing.Engine;
using Marktek.Fluent.Testing.Engine.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MarkTek.Fluent.Testing.RecordGeneration
{
    /// <summary>
    /// Base Record Service
    /// </summary>
    /// <typeparam name="TID"></typeparam>
    public class RecordService<TID> : IRecordService<TID>
    {

        private List<TID> CreatedIds { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public TID AggregateId { get; private set; }

        /// <summary>
        /// Every Graph must have a hierarchy
        /// </summary>
        /// <param name="aggregateId"></param>
        public RecordService(TID aggregateId)
        {
            CreatedIds = new List<TID> { aggregateId};
            this.AggregateId = aggregateId;
        }

        /// <summary>
        /// Creates a record implementing a class that implements IIRecordCreator<typeparamref name="TEntity"/>
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="app"></param>
        /// <returns></returns>
        public virtual IRecordService<TID> CreateRecord<TEntity>(IRecordCreator<TEntity, TID> app)
        {
            this.CreatedIds.Add(app.CreateRecord().Id);
            return this;
        }

        /// <summary>
        /// Creates a releated record from the previous CreateRecordMethod
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="app"></param>
        /// <returns></returns>
        public virtual IRecordService<TID> CreateRelatedRecord<TEntity>(IRelatedRecordCreator<TEntity, TID> app)
        {
            Guard.AgainstNull(this.CreatedIds);
            this.CreatedIds.Add(app.CreateRecord(CreatedIds.Last()).Id);
            return this;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TType"></typeparam>
        /// <param name="spec"></param>
        /// <returns></returns>
        public IRecordService<TID> AssertAgainst<TType>(BaseValidator<TID, TType> spec) 
        {
            spec.Validate(AggregateId);
            return this;
        }

        
        /// <summary>
        /// Execute method based on Condition. Useful for Scenarios where you want to configure the behaviour
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="builder"></param>
        /// <returns></returns>
        public IRecordService<TID> If(bool condition, Func<IRecordService<TID>, IRecordService<TID>> builder)
        {
            return condition ? builder(this) : this;
        }

        /// <summary>
        /// Cleanup Records
        /// </summary>
        /// <param name="Id"></param>
        public void Cleanup(IRecordCleanup<TID> Id)
        {
            Id.Cleanup(AggregateId);
        }

    }
}