using Marktek.Fluent.Testing.Engine.Interfaces;
using MarkTek.Fluent.Testing.RecordGeneration;
using Microsoft.Azure.Cosmos;
using System.Collections.Generic;


namespace FluidTest.CosmosDB
{
    public class DropCosmosContainer : IRecordCleanup<string>
    {
        private string databaseName;
        private string container;
        private CosmosClient client;

        public DropCosmosContainer(string databaseName, string container, CosmosClient client)
        {
            this.databaseName = databaseName;
            this.container = container;
            this.client = client;
        }

        public void Cleanup(List<Record<object, string>> records, string aggregateId)
        {
            client.GetDatabase(databaseName).GetContainer(this.container).DeleteContainerAsync().Wait();
        }
    }
}
