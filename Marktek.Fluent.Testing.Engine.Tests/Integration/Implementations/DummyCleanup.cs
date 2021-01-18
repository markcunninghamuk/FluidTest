using Marktek.Fluent.Testing.Engine.Interfaces;
using System;
using System.Collections.Generic;

namespace Marktek.Fluent.Testing.Engine.Tests
{
    internal class DummyCleanup : IRecordCleanup<Guid>
    {
        public void Cleanup(Dictionary<Guid, object> records, Guid aggregateId)
        {
            Console.WriteLine("Cleaning up records");
        }
    }
}