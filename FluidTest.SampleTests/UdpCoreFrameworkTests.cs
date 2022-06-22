using FluidTest.AzureDataFactory.Executors;
using FluidTest.AzureStorage.Cleanup;
using FluidTest.AzureStorage.PreExecution;
using FluidTest.AzureSynapse.WaitActions;
using FluidTest.CosmosDB;
using FluidTest.CosmosDB.PreExecution;
using FluidTest.SampleTests.Base;
using MarkTek.Fluent.Testing.RecordGeneration;
using Microsoft.Azure.Cosmos;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace FluidTest.SampleTests
{
    [TestClass]
    public class UdpCoreFrameworkTests: TestExecutionBase
    {

        [TestMethod]
        public void Test_DataFramework_With_FullDataSet()
        {
            var databaseName = "UdpTesting";
            var containerName = "containerName";

            var dataLakeContainerName = "datanalytics";
            var folderPath = "RAW/GENERIC/TestFullDataSet";

            //Test scenario Set Up
            recordService
               .PreExecutionAction(new CreateCosmosDatabaseIfNotExists(CosmosClient, databaseName, ThroughputProperties.CreateAutoscaleThroughput(4000)))
               .PreExecutionAction(new CreateCosmosContainerIfNotExists(CosmosClient, databaseName, new ContainerProperties { Id = containerName, PartitionKeyPath = "/id" }, 4000))
               .PreExecutionAction(new CreateFolderOnDataLakeContainerIfNotExists(containerName, folderPath, DataLakeClient))

               .Cleanup(new DropCosmosDatabase(databaseName, CosmosClient))
               .Cleanup(new DropDataLakeFolder(dataLakeContainerName, folderPath, DataLakeClient));

        }
    }
}
