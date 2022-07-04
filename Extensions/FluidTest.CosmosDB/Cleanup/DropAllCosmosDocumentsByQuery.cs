using Marktek.Fluent.Testing.Engine.Interfaces;
using MarkTek.Fluent.Testing.RecordGeneration;
using Microsoft.Azure.Cosmos;
using System.Collections.Generic;


namespace FluidTest.CosmosDB
{
    public class DropAllCosmosDocumentsByQuery : IRecordCleanup<string>
    {
        private string databaseName;
        private string container;
        private CosmosClient client;
        private string query;

        public DropAllCosmosDocumentsByQuery(CosmosClient client, string databaseName, string container, string query)
        {
            this.databaseName = databaseName;
            this.container = container;
            this.client = client;
            this.query = query;
        }

        public void Cleanup(List<Record<object, string>> records, string aggregateId)
        {
            var document = this.client.GetContainer(databaseName, this.container).GetItemQueryIterator<object>(query);

            List<dynamic> docs = new List<dynamic>();

            while (document.HasMoreResults)
            {
                var response = document.ReadNextAsync().GetAwaiter().GetResult();

                foreach (dynamic item in response)
                {
                    var container = client.GetContainer(this.databaseName, this.container);
                    if (container != null)
                        container.DeleteItemAsync<dynamic>(item.id.Value.ToString(), new PartitionKey(item.id.Value)).Wait();
                }
            }

        }
    }
}
