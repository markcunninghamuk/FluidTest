using Marktek.Fluent.Testing.Engine.Interfaces;
using Microsoft.Azure.Cosmos;

namespace FluidTest.CosmosDB.PreExecution
{
    class CreateCosmosContainer : IPreExecution
    {
        private CosmosClient client;
        private string databaseName;
        private int throughput;
        private ContainerProperties containerProperties;

        public CreateCosmosContainer(CosmosClient client, string databaseName,ContainerProperties containerProperties, int throughput)
        {
            this.client = client;
            this.databaseName = databaseName;
            this.throughput = throughput;
            this.containerProperties = containerProperties;
        }

        public void Execute()   
        {
            Database targetDatabase = client.GetDatabase(databaseName);
            targetDatabase.CreateContainerIfNotExistsAsync(containerProperties, throughput).GetAwaiter().GetResult();
        }
    }
}
