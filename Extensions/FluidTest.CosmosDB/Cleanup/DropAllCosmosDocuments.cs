using Marktek.Fluent.Testing.Engine.Interfaces;
using MarkTek.Fluent.Testing.RecordGeneration;
using Microsoft.Azure.Cosmos;
using System.Collections.Generic;


namespace FluidTest.CosmosDB
{
    public class DropAllCosmosDocuments : IRecordCleanup<string>
    {
        private string databaseName;
        private string container;
        private CosmosClient client;

        public DropAllCosmosDocuments(string databaseName, string container, CosmosClient client)
        {
            this.databaseName = databaseName;
            this.container = container;
            this.client = client;
        }

        public void Cleanup(List<Record<object, string>> records, string aggregateId)
        {
            var document = this.client.GetContainer(databaseName, this.container).GetItemQueryIterator<object>("select * from c");

            List<dynamic> docs = new List<dynamic>();

            while (document.HasMoreResults)
            {
                var response = document.ReadNextAsync().GetAwaiter().GetResult();
                docs.AddRange(response.Resource);
            }

            docs.ForEach(d =>
            {                
                client.GetContainer(this.databaseName, this.container).DeleteItemAsync(d.id.ToString(), d.id);
            });
        }
    }
}
