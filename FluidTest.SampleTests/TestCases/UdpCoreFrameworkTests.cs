using FluidTest.AzureDataFactory.Executors;
using FluidTest.AzureStorage.Cleanup;
using FluidTest.AzureStorage.PreExecution;
using FluidTest.AzureSynapse.Assertions;
using FluidTest.AzureSynapse.WaitActions;
using FluidTest.CosmosDB;
using FluidTest.CosmosDB.PreExecution;
using FluidTest.SampleTests.Base;
using MarkTek.Fluent.Testing.RecordGeneration;
using Microsoft.Azure.Cosmos;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace FluidTest.SampleTests.TestCases
{
    [TestClass]
    public class UdpCoreFrameworkTests: TestExecutionBase
    {

        [TestMethod]
        public void Test_DataFramework_With_FullDataSet()
        {
            var databaseName = "UdpTesting";
            var cosmosDbContainerName = "TestFullLoad";

            var dataLakeContainerName = "defraanalyticsdata";
            var folderPathRawGeneric = "RAW/GENERIC/TestFullLoad";
            var folderPathConfig = "CONFIG/TestFullLoad";

            recordService
               .PreExecutionAction(new CreateCosmosDatabaseIfNotExists(CosmosClient, databaseName, ThroughputProperties.CreateAutoscaleThroughput(4000)))
               .PreExecutionAction(new CreateCosmosContainerIfNotExists(CosmosClient, databaseName, new ContainerProperties { Id = cosmosDbContainerName, PartitionKeyPath = "/id" }, 4000))
               .PreExecutionAction(new CreateFolderOnDataLakeContainerIfNotExists(dataLakeContainerName, folderPathConfig, DataLakeClient))
               .PreExecutionAction(new DropFileToDataLake(dataLakeContainerName, folderPathConfig, "UdpCoreFrameworkTests/Config/TestFullLoad.json", DataLakeClient))
               .PreExecutionAction(new CreateFolderOnDataLakeContainerIfNotExists(dataLakeContainerName, folderPathRawGeneric, DataLakeClient))
               .PreExecutionAction(new DropFileToDataLake(dataLakeContainerName, folderPathRawGeneric, "UdpCoreFrameworkTests/Data/TestFullLoad-new.json", "TestFullLoad.json", DataLakeClient));
            //.CreateRecord(new GetTriggeredPipeline("PL_RAW_To_STAGING_Generic", PipelineRunClient, DateTimeOffset.UtcNow), DefaultRetryPolicy)
            //.AssignAggregateId()
            //.WaitFor(new WaitForPipelineStatus(PipelineRunClient, recordService.GetAggregateId(), "Succeeded"), DefaultRetryPolicy)
            //.AssertAgainst(new VerifyAzureSynapsePipelineStatus("Succeeded", PipelineRunClient));
            //.Cleanup(new DropAllCosmosDocumentsByQuery(databaseName, cosmosDbContainerName, CosmosClient, "select * from c"))
            //.Cleanup(new DropCosmosDatabase(databaseName, CosmosClient))
            //.Cleanup(new DropDataLakeFolder(dataLakeContainerName, folderPathRawGeneric, DataLakeClient))
            //.Cleanup(new DropDataLakeFolder(dataLakeContainerName, folderPathConfig, DataLakeClient));

        }
    }
}
