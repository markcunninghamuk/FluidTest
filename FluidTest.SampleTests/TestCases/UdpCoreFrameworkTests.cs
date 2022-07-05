using FluidTest.AzureStorage.Cleanup;
using FluidTest.AzureStorage.PreExecution;
using FluidTest.AzureSynapse.Assertions;
using FluidTest.AzureSynapse.Executors;
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
    public class UdpCoreFrameworkTests : TestExecutionBase
    {

        [TestMethod]
        public void Test_DataFramework_With_FullDataSet()
        {
            var databaseName = "UdpTesting";
            var cosmosDbContainerName = "TestFullLoad";

            var dataLakeContainerName = "defraanalyticsdata";
            var folderPathRawGeneric = "RAW/GENERIC/TestFullLoad";
            var folderPathConfig = "CONFIG/TestFullLoad";
            var folderPathDelta = "PROCESSED/COMPLETED/TestFullLoad";

            var startTime = DateTimeOffset.UtcNow;

            recordService
               .PreExecutionAction(new CreateCosmosDatabaseIfNotExists(CosmosClient, databaseName, ThroughputProperties.CreateAutoscaleThroughput(4000)))
               .PreExecutionAction(new CreateCosmosContainerIfNotExists(CosmosClient, databaseName, new ContainerProperties { Id = cosmosDbContainerName, PartitionKeyPath = "/id" }, 4000))
               .PreExecutionAction(new CreateFolderOnDataLakeContainerIfNotExists(dataLakeContainerName, folderPathConfig, DataLakeClient))
               .PreExecutionAction(new DropFileToDataLake(dataLakeContainerName, folderPathConfig, "UdpCoreFrameworkTests/Config/TestFullLoad.json", DataLakeClient))
               .PreExecutionAction(new CreateFolderOnDataLakeContainerIfNotExists(dataLakeContainerName, folderPathRawGeneric, DataLakeClient))
               .Cleanup(new DropDataLakeFolder(dataLakeContainerName, folderPathDelta, DataLakeClient))
               .Cleanup(new DropAllCosmosDocumentsByQuery(CosmosClient, databaseName, cosmosDbContainerName, "select * from c"));


            //Execute New Load
            startTime = DateTimeOffset.UtcNow;
            recordService
               .PreExecutionAction(new DropFileToDataLake(dataLakeContainerName, folderPathRawGeneric, "UdpCoreFrameworkTests/Data/TestFullLoad-full.json", "TestFullLoad.json", DataLakeClient))
               .CreateRecord(new GetTriggeredPipeline("PL_RAW_To_STAGING_Generic", PipelineRunClient, startTime), DefaultRetryPolicy)
               .AssignAggregateId()
               .WaitFor(new WaitForPipelineStatus(PipelineRunClient, recordService.GetAggregateId(), "Succeeded"), DefaultRetryPolicy)
               .CreateRecord(new GetTriggeredPipeline("PL_Config_Based_Processing", PipelineRunClient, startTime), DefaultRetryPolicy)
               .AssignAggregateId()
               .WaitFor(new WaitForPipelineStatus(PipelineRunClient, recordService.GetAggregateId(), "Succeeded"), DefaultRetryPolicy)
               .CreateRecord(new GetTriggeredPipeline("PL_Config_Based_publish", PipelineRunClient, startTime), DefaultRetryPolicy)
               .AssignAggregateId()
               .WaitFor(new WaitForPipelineStatus(PipelineRunClient, recordService.GetAggregateId(), "Succeeded"), DefaultRetryPolicy);

            //Execute Update
            startTime = DateTimeOffset.UtcNow;
            recordService
               .PreExecutionAction(new DropFileToDataLake(dataLakeContainerName, folderPathRawGeneric, "UdpCoreFrameworkTests/Data/TestFullLoad-updated.json", "TestFullLoad.json", DataLakeClient))
               .CreateRecord(new GetTriggeredPipeline("PL_RAW_To_STAGING_Generic", PipelineRunClient, startTime), DefaultRetryPolicy)
               .AssignAggregateId()
               .WaitFor(new WaitForPipelineStatus(PipelineRunClient, recordService.GetAggregateId(), "Succeeded"), DefaultRetryPolicy)
               .CreateRecord(new GetTriggeredPipeline("PL_Config_Based_Processing", PipelineRunClient, startTime), DefaultRetryPolicy)
               .AssignAggregateId()
               .WaitFor(new WaitForPipelineStatus(PipelineRunClient, recordService.GetAggregateId(), "Succeeded"), DefaultRetryPolicy)
               .CreateRecord(new GetTriggeredPipeline("PL_Config_Based_publish", PipelineRunClient, startTime), DefaultRetryPolicy)
               .AssignAggregateId()
               .WaitFor(new WaitForPipelineStatus(PipelineRunClient, recordService.GetAggregateId(), "Succeeded"), DefaultRetryPolicy);

            //Execute delete
            startTime = DateTimeOffset.UtcNow;
            recordService
               .PreExecutionAction(new DropFileToDataLake(dataLakeContainerName, folderPathRawGeneric, "UdpCoreFrameworkTests/Data/TestFullLoad-delete.json", "TestFullLoad.json", DataLakeClient))
               .CreateRecord(new GetTriggeredPipeline("PL_RAW_To_STAGING_Generic", PipelineRunClient, startTime), DefaultRetryPolicy)
               .AssignAggregateId()
               .WaitFor(new WaitForPipelineStatus(PipelineRunClient, recordService.GetAggregateId(), "Succeeded"), DefaultRetryPolicy)
               .CreateRecord(new GetTriggeredPipeline("PL_Config_Based_Processing", PipelineRunClient, startTime), DefaultRetryPolicy)
               .AssignAggregateId()
               .WaitFor(new WaitForPipelineStatus(PipelineRunClient, recordService.GetAggregateId(), "Succeeded"), DefaultRetryPolicy)
               .CreateRecord(new GetTriggeredPipeline("PL_Config_Based_publish", PipelineRunClient, startTime), DefaultRetryPolicy)
               .AssignAggregateId()
               .WaitFor(new WaitForPipelineStatus(PipelineRunClient, recordService.GetAggregateId(), "Succeeded"), DefaultRetryPolicy);

            //Execute add new record
            startTime = DateTimeOffset.UtcNow;
            recordService
               .PreExecutionAction(new DropFileToDataLake(dataLakeContainerName, folderPathRawGeneric, "UdpCoreFrameworkTests/Data/TestFullLoad-new.json", "TestFullLoad.json", DataLakeClient))
               .CreateRecord(new GetTriggeredPipeline("PL_RAW_To_STAGING_Generic", PipelineRunClient, startTime), DefaultRetryPolicy)
               .AssignAggregateId()
               .WaitFor(new WaitForPipelineStatus(PipelineRunClient, recordService.GetAggregateId(), "Succeeded"), DefaultRetryPolicy)
               .CreateRecord(new GetTriggeredPipeline("PL_Config_Based_Processing", PipelineRunClient, startTime), DefaultRetryPolicy)
               .AssignAggregateId()
               .WaitFor(new WaitForPipelineStatus(PipelineRunClient, recordService.GetAggregateId(), "Succeeded"), DefaultRetryPolicy)
               .CreateRecord(new GetTriggeredPipeline("PL_Config_Based_publish", PipelineRunClient, startTime), DefaultRetryPolicy)
               .AssignAggregateId()
               .WaitFor(new WaitForPipelineStatus(PipelineRunClient, recordService.GetAggregateId(), "Succeeded"), DefaultRetryPolicy);

            //.AssertAgainst(new VerifyAzureSynapsePipelineStatus("Succeeded", PipelineRunClient));
            recordService
               .Cleanup(new DropCosmosDatabase(CosmosClient, databaseName))
               .Cleanup(new DropDataLakeFolder(dataLakeContainerName, folderPathRawGeneric, DataLakeClient))
               .Cleanup(new DropDataLakeFolder(dataLakeContainerName, folderPathConfig, DataLakeClient));

        }
    }
}
