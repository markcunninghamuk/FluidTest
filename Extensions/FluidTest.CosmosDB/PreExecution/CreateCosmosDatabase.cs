using Marktek.Fluent.Testing.Engine.Interfaces;
using Microsoft.Azure.Cosmos;
using System;

namespace FluidTest.CosmosDB.PreExecution
{
    class CreateCosmosDatabase : IPreExecution
    {
        private CosmosClient client;
        private string databaseName;
        private ThroughputProperties properties;

        public CreateCosmosDatabase(CosmosClient client, string databaseName, ThroughputProperties properties)
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
