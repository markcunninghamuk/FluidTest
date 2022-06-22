using Marktek.Fluent.Testing.Engine.Interfaces;
using MarkTek.Fluent.Testing.RecordGeneration;
using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Text;

namespace FluidTest.CosmosDB
{
    public class DropCosmosDatabase : IRecordCleanup<string>
    {
        private string databaseName;
        private CosmosClient client;

        public DropCosmosDatabase(string databaseName, CosmosClient client)
        {
            this.databaseName = databaseName;
            this.client = client;
        }

        public void Cleanup(List<Record<object, string>> records, string aggregateId)
        {
            client.GetDatabase(databaseName).DeleteAsync().Wait();
        }
    }
}
