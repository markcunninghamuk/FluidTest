using Azure.Analytics.Synapse.Artifacts;
using Azure.Identity;
using Azure.Storage.Files.DataLake;
using System;
using Azure.Core;
using Microsoft.Azure.Cosmos;
using MarkTek.Fluent.Testing.RecordGeneration;
using System.Collections.Generic;
using FluidTest.AzureStorage.PreExecution;
using FluidTest.AzureSynapse.Executors;
using FluidTest.AzureSynapse.WaitActions;
using FluidTest.AzureSynapse.Assertions;
using Polly;
using Marktek.Fluent.Testing.Engine.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluidTest.SampleTests.Base
{
    public abstract class TestExecutionBase
    {
        protected IRecordService<string> recordService;

        protected const string CONTAINER_NAME = "defraanalyticsdata";
        private Uri SynapseUri() => new Uri(Environment.GetEnvironmentVariable("SynapseUri"));

        private Uri StorageUri() => new Uri(Environment.GetEnvironmentVariable("StorageUri"));

        public PipelineClient PipelineClient => new PipelineClient(SynapseUri(), ClientCredentials);

        public ClientSecretCredential ClientCredentials => new ClientSecretCredential(Environment.GetEnvironmentVariable("TenantId"), Environment.GetEnvironmentVariable("SpClientId"), Environment.GetEnvironmentVariable("SpClientSecret"), new TokenCredentialOptions());

        public TokenCredential TokenCredentials => new ClientSecretCredential(Environment.GetEnvironmentVariable("TenantId"), Environment.GetEnvironmentVariable("SpClientId"), Environment.GetEnvironmentVariable("SpClientSecret"), new TokenCredentialOptions());

        public PipelineRunClient PipelineRunClient => new PipelineRunClient(SynapseUri(), ClientCredentials);

        public DataLakeServiceClient DataLakeClient => new DataLakeServiceClient(StorageUri(), TokenCredentials);

        public Policy DefaultRetryPolicy => Policy
            .Handle<ExecutionWaitException>()
            .WaitAndRetry(2, retryAttempt => TimeSpan.FromSeconds(5));

        public CosmosClient CosmosClient => new CosmosClient(Environment.GetEnvironmentVariable("CosmosDbEndpoint"), TokenCredentials);

        [TestInitialize]
        public void Setup()
        {
            this.recordService = new RecordService<string>(string.Empty);
        }

        protected void Execute_Test_For_Valid_Trigger(IRecordService<string> recordService, Policy policy, string pipelineName, Dictionary<string, object> parameters)
        {
            recordService
           .CreateRecord(new RunPipeline(pipelineName, parameters, PipelineClient), policy)
           .AssignAggregateId()
           .WaitFor(new WaitForPipelineStatus(PipelineRunClient, recordService.GetAggregateId(), "Succeeded"), policy)
           .AssertAgainst(new VerifyAzureSynapsePipelineStatus("Succeeded", PipelineRunClient));
        }

        protected void Execute_Test_With_Trigger(IRecordService<string> recordService, Policy policy, string pipelineName, List<StorageFileDetails> filedetails)
        {
            foreach (var item in filedetails)
            {
                recordService
                      .PreExecutionAction(new DropFileToDataLake(CONTAINER_NAME, item.FolderPath, item.FileName, DataLakeClient));
            }
            recordService
           .CreateRecord(new GetTriggeredPipeline(pipelineName, PipelineRunClient, DateTimeOffset.UtcNow), policy)
           .AssignAggregateId()
           .WaitFor(new WaitForPipelineStatus(PipelineRunClient, recordService.GetAggregateId(), "Succeeded"), policy)
           .AssertAgainst(new VerifyAzureSynapsePipelineStatus("Succeeded", PipelineRunClient));
        }

        protected void Execute_Test_By_Dropping_Files(IRecordService<string> recordService, Policy policy, string pipelineName, Dictionary<string, object> parameters, List<StorageFileDetails> filedetails)
        {
            foreach (var item in filedetails)
            {
                recordService
                      .PreExecutionAction(new DropFileToDataLake(CONTAINER_NAME, item.FolderPath, item.FileName, DataLakeClient));
            }

            Execute_Test_For_Valid_Trigger(recordService, policy, pipelineName, parameters);
        }

    }
}
