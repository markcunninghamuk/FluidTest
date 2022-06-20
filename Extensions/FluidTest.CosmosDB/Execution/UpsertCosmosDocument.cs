using MarkTek.Fluent.Testing.RecordGeneration;
using Microsoft.Azure.Cosmos;
using System;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace FluidTest.CosmosDB.Execution
{
    public class UpsertCosmosDocument<T> : IRecordCreator<T, string>
    {
        private CosmosClient client;
        private string databaseName;
        private string containerName;
        private T item;
        private PartitionKey? partitionKey;

        public UpsertCosmosDocument(CosmosClient client, string databaseName, string containerName, T item, PartitionKey partitionkey)
        {
            this.client = client;
            this.databaseName = databaseName;
            this.containerName = containerName;
            this.item = item;
            this.partitionKey = partitionkey;
        }

        public Record<T, string> CreateRecord()
        {
            var container = this.client.GetContainer(databaseName, containerName);

            var res = container.UpsertItemAsync(this.item, this.partitionKey).GetAwaiter().GetResult();

            var value = JObject.Parse(res.Resource.ToString());
            var id = ((JValue)value.Property("id").Value).Value;

            return new Record<T, string>(res, id.ToString(), "createdRecords");
        }
    }
}