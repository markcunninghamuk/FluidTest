using Marktek.Fluent.Testing.Engine.Interfaces;
using MarkTek.Fluent.Testing.RecordGeneration;
using System;
using System.Collections.Generic;

namespace Marktek.Fluent.Testing.Engine.Tests
{
    internal class DummyCleanup : IRecordCleanup<Guid>
    {
      
        public void Cleanup(List<Record<object, Guid>> records, Guid aggregateId)
        {
            Console.WriteLine($"Cleaning up {records.Count} records using root id {aggregateId}");
        }
    }
}