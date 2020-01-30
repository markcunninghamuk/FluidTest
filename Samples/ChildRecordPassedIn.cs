using System;
using MarkTek.Fluent.Testing.ExportApplications.Model;
using MarkTek.Fluent.Testing.RecordGeneration;

namespace Marktek.Fluent.Testing.Engine.Sample
{
    internal class ChildRecordPassedIn : IRelatedRecordCreator<Order,ChildRecordPassedIn, Guid>
    {      
       
        Record<ChildRecordPassedIn, Guid> IRelatedRecordCreator<Order, ChildRecordPassedIn, Guid>.CreateRecord(Order entity)
        {
            Console.WriteLine($"Creating Child reord eith parent id of {entity.Id}");
            return new Record<ChildRecordPassedIn, Guid>(new ChildRecordPassedIn(), Guid.NewGuid());
        }
    }
}