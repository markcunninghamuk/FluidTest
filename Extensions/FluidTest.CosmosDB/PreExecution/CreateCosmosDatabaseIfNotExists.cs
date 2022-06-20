using Marktek.Fluent.Testing.Engine.Interfaces;
using Microsoft.Azure.Cosmos;

namespace FluidTest.CosmosDB.PreExecution
{
    public class CreateCosmosDatabaseIfNotExists : IPreExecution
    {
        private CosmosClient client;
        private string databaseName;
        private ThroughputProperties properties;

        public CreateCosmosDatabaseIfNotExists(CosmosClient client, string databaseName, ThroughputProperties properties)
        {
            this.client = client;
            this.databaseName = databaseName;
            this.properties = properties;
        }

        public void Execute()
        {
            client.CreateDatabaseIfNotExistsAsync(databaseName, properties).GetAwaiter().GetResult();
        }
    }
}
