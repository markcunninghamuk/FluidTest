using Marktek.Fluent.Testing.Engine.Tests.Models;
using MarkTek.Fluent.Testing.RecordGeneration;
using System;

namespace Marktek.Fluent.Testing.Engine.Tests
{
    internal class CreateDummyExampleRelated : IRelatedRecordCreator<DummyRelatedModel, Guid>, IRelatedRecordCreator<DummyModel,DummyRelatedModel,Guid>
    {
        public bool SupportsRetry() => true;

        public Record<DummyRelatedModel, Guid> CreateRecord(Guid id)
        {
            var dummy = new DummyRelatedModel
            {
                Id = Guid.NewGuid(),
                ParentId = id
            };

            Console.WriteLine("Creating related records");

            return new Record<DummyRelatedModel, Guid>(dummy, dummy.Id, new Action<Guid>(doIt));
        }

        private void doIt(Guid obj)
        {
           
        }

        public Record<DummyRelatedModel, Guid> CreateRecord(DummyModel entity)
        {
            var dummy = new DummyRelatedModel
            {
                Id = Guid.NewGuid(),
                ParentId = entity.Id
            };

            Console.WriteLine("Creating related records");

            return new Record<DummyRelatedModel, Guid>(dummy, dummy.Id, new Action<Guid>(doIt));
        }
    }
}