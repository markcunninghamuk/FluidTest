﻿using Marktek.Fluent.Testing.Engine.Tests.Models;
using MarkTek.Fluent.Testing.RecordGeneration;
using System;
using System.IO;

namespace Marktek.Fluent.Testing.Engine.Tests
{
    internal class CreateDummyExample : IRecordCreator<DummyModel, Guid>
    {
        public Guid Id { get; set; }

        public CreateDummyExample()
        {
            this.Id = Guid.NewGuid();
        }

            public CreateDummyExample(Guid Id)
        {
            this.Id = Id;
        }

        public Record<DummyModel, Guid> CreateRecord()
        {
              return new Record<DummyModel, Guid>(new DummyModel(), this.Id, "alias");
        }


    }

    internal class CreateDummyExampleWithCleanupHandler : IRecordCreator<DummyModel, Guid>
    {

        public Record<DummyModel, Guid> CreateRecord()
        {
            return new Record<DummyModel, Guid>(new DummyModel(), Guid.NewGuid(), "alias2", CleanupHandler);
        }

        private void CleanupHandler(Guid obj)
        {
            Console.WriteLine($"Cleaning up Record using the reference : {obj }");
        }
    }
}