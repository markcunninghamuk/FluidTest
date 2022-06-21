using FluidTest.CosmosDB.Assertions;
using MarkTek.Fluent.Testing.RecordGeneration;
using Microsoft.Azure.Cosmos;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluidTest.CosmosDB.PreExecution;
using FluidTest.CosmosDB;
using FluidTest.CosmosDB.Execution;
using System;

namespace FluidTest.Samples
{   
    [TestClass]
    public class CosmosTests
    {
        private CosmosClient client;
      
        [TestInitialize]
        public void Setup()
        {
            this.client = new CosmosClient("AccountEndpoint=https://marktek-cosmos.documents.azure.com:443/;AccountKey=RVPoBk6OCy39fELcRR11aIsLKbsWJVD6c5W0r4truBJDZaosFnoadPaYjcZV6buRAD8hacg6xG8qkM3ttGakgQ==;");
        }

        //Create a database and a container and clean it up at the end
        [DataTestMethod]
        [DataRow("myContainerName", "myDatabaseName",4000)]
        public void Create_Database_And_Container_Then_Cleanup(string containerName, string databaseName, int throughput)
        {
            var dynamicRecord = new { id = Guid.NewGuid().ToString(), name = "test" };

            var recordService = new RecordService<string>(databaseName);

            recordService
               .PreExecutionAction(new CreateCosmosDatabaseIfNotExists(client, databaseName, ThroughputProperties.CreateAutoscaleThroughput(throughput)))
               .PreExecutionAction(new CreateCosmosContainerIfNotExists(client, databaseName, new ContainerProperties { Id = containerName, PartitionKeyPath = "/id" }, 4000))
               .CreateRecord(new UpsertCosmosDocument<dynamic>(client, databaseName, containerName, dynamicRecord, new PartitionKey(dynamicRecord.id)))
               .AssertAgainst(new CosmosContainerShouldExist(this.client, containerName, databaseName))
               .Cleanup(new DropCosmosDatabase(databaseName,client));
        }
    }
}
