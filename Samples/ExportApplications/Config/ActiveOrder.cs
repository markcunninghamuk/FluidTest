using MarkTek.Fluent.Testing.RecordGeneration;
using MarkTek.Fluent.Testing.ExportApplications.Model;
using System;
using System.IO;

namespace MarkTek.Fluent.Testing.Sample.Specifications.Config
{
    public class ActiveOrderConfiguration : IRecordCreator<Order, Guid>, IRelatedRecordCreator<Order, Guid>
    {
        private Guid id;

        public ActiveOrderConfiguration(Guid aggregateId)
        {
            this.id = aggregateId;
        }

        public Record<Order, Guid> CreateRecord()
        {
            var c = new Order();
          
            Console.WriteLine($"Creating Order");

            File.AppendAllLines("C:\\Test\\test.txt",new[] { "Creating Order" });

            return new Record<Order, Guid>(c, id);
        }

        public Record<Order, Guid> CreateRecord(Guid id)
        {

            //order.AccountId = new EntityReference("account", id);


            var c = new Order();
            Console.WriteLine($"Creating related Order with parent id {id}");


            File.AppendAllLines("C:\\Test\\test.txt", new[] { $"Creating related Order with parent id {id}" });

            return new Record<Order, Guid>(c, c.Id);
        }

    }
}
