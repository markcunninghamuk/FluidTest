using Marktek.Fluent.Testing.Engine.Interfaces;
using System;

namespace Marktek.Fluent.Testing.Engine.Sample.ExportApplications.Cleanup
{
    public class Cleanup : IRecordCleanup<Guid>
    {        
        void IRecordCleanup<Guid>.Cleanup(Guid AggregateId)
        {
            Console.WriteLine($"clean record using {AggregateId}");
        }
    }
}
