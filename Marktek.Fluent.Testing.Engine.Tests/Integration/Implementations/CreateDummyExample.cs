using Marktek.Fluent.Testing.Engine.Tests.Models;
using MarkTek.Fluent.Testing.RecordGeneration;
using System;

namespace Marktek.Fluent.Testing.Engine.Tests
{
    internal class CreateDummyExample : IRecordCreator<DummyModel, Guid>
    {
        public Record<DummyModel, Guid> CreateRecord()
        {
            return new Record<DummyModel, Guid>(new DummyModel(), Guid.NewGuid());
        }
    }
}