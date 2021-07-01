using Marktek.Fluent.Testing.Engine.Tests.Models;
using MarkTek.Fluent.Testing.RecordGeneration;
using System;
using System.Collections.Generic;

namespace Marktek.Fluent.Testing.Engine.Tests
{
    internal class CreateDummyExampleComposite : IRelatedRecordCreatorComposite<DummyModel, Guid>
    {      

        public Record<DummyModel, Guid> CreateRecord(List<Guid> id)
        {
            var ob = new
            {
                key1 = id[0],
                key2 = id[1]
            };

            return new Record<DummyModel, Guid>(new DummyModel(), Guid.NewGuid());

        }
    }
}