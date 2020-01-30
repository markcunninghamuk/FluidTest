using Marktek.Fluent.Testing.Engine.Interfaces;
using MarkTek.Fluent.Testing.ExportApplications.Model;
using System;
using System.Collections.Generic;

namespace Marktek.Fluent.Testing.Engine.Sample.ExportApplications.Cleanup
{
    public class Cleanup : IRecordCleanup<Guid>
    {        
        void IRecordCleanup<Guid>.Cleanup(Dictionary<Guid, object> records)
        {
            foreach (var item in records)
            {
                if (item.Value is Order)
                {
                    Order o = item.Value as Order;
                    Console.WriteLine($"Deleting order {o.Id}");
                }
            }
        }
    }
}