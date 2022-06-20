using MarkTek.Fluent.Testing.RecordGeneration;
using Microsoft.Azure.Cosmos;
using System;

namespace FluidTest.CosmosDB.Execution
{
    class UpsertCosmosDocument<T> : IRecordCreator<ItemResponse<T>, Guid>
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
        
        Record<ItemResponse<T>, Guid> IRecordCreator<ItemResponse<T>, Guid>.CreateRecord()
        {
            var container = this.client.GetContainer(databaseName, containerName);
            var item = container.UpsertItemAsync(this.item, this.partitionKey);
            //TODO figure how to get the document id.
            //  return new Record<ItemResponse<T>, Guid>(item,item,"createdCosmosRecord");
            return null;
        }
    }
}