using FluidTest.CosmosDB.Assertions;
using MarkTek.Fluent.Testing.RecordGeneration;
using Microsoft.Azure.Cosmos;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluidTest.CosmosDB.PreExecution;
using FluidTest.CosmosDB;
using FluidTest.CosmosDB.Execution;
using System;
using FluidTest.SampleTests.Base;
using System.Collections.Generic;

namespace FluidTest.Samples.TestCases
{
    [Ignore]
    [TestClass]
    public class CosmosTests: TestExecutionBase
    {
        //Create a database and a container and clean it up at the end
        [DataTestMethod]
        [DataRow("myContainerName", "myDatabaseName",4000)]
        public void Create_Database_And_Container_Then_Cleanup(string containerName, string databaseName, int throughput)
        {
            var dynamicRecord = new { id = Guid.NewGuid().ToString(), name = "test" };
            var dynamicRecord1 = new { id = Guid.NewGuid().ToString(), name = "test1" };

            recordService
                   .PreExecutionAction(new CreateCosmosDatabaseIfNotExists(CosmosClient, databaseName, ThroughputProperties.CreateAutoscaleThroughput(throughput)))
                   .PreExecutionAction(new CreateCosmosContainerIfNotExists(CosmosClient, databaseName, new ContainerProperties { Id = containerName, PartitionKeyPath = "/id" }, 4000))
                   .CreateRecord(new UpsertCosmosDocument<dynamic>(CosmosClient, databaseName, containerName, dynamicRecord, new PartitionKey(dynamicRecord.id)))
                   .CreateRecord(new UpsertCosmosDocument<dynamic>(CosmosClient, databaseName, containerName, dynamicRecord1, new PartitionKey(dynamicRecord1.id)))
                   .AssertAgainst(new VerifyCosmosRecords(CosmosClient, databaseName, containerName, "select * from c", 2, new Dictionary<string, string>()))
                   .AssertAgainst(new CosmosContainerShouldExist(CosmosClient, containerName, databaseName))
                   .Cleanup(new DropAllCosmosDocumentsByQuery(CosmosClient, databaseName, containerName, "select * from c where c.name='test1'"))
                   .AssertAgainst(new VerifyCosmosRecords(CosmosClient, databaseName, containerName, "select * from c", 1, new Dictionary<string, string>()))
                   .Cleanup(new DropCosmosDatabase(CosmosClient, databaseName));
        }
    }
}
