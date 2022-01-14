## Intro

This example shows how you can drop a file to datalake and test the trigger runs, it also waits for the pipeline to succeed allowing you to test end to end.

```cs
using FluidTest.AzureStorage.PreExecution;
using FluidTest.AzureSynapse.Executors;
using FluidTest.AzureSynapse.WaitActions;
using MarkTek.Fluent.Testing.RecordGeneration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Polly;
using System;
using Marktek.Fluent.Testing.Engine.Exceptions;

namespace FluidTestSynapse
{
    [TestClass]
    [TestCategory("Integration")]
    public class PipelineExecutonTests : TestExecutionBase
    {
        private IRecordService<string> recordService;
        private Policy policy = Policy
               .Handle<ExecutionWaitException>()
               .WaitAndRetry(60, retryAttempt => TimeSpan.FromSeconds(30));

        [TestInitialize]
        public void Setup()
        {
            this.recordService = new RecordService<string>(string.Empty);
        }

        [DataTestMethod]
        [DataRow("PL_PipelineTest", "data.csv", "RAW")]
        public void Pipeline_Should_Succeed_For_Valid_File_Trigger(string pipelineName, string sourceFilePath, string rawFolder)
        {
            recordService
                .PreExecutionAction(new DropFileToDataLake("lake", rawFolder, sourceFilePath, DataLakeClient))
                .CreateRecord(new GetTriggeredPipeline(pipelineName, PipelineRunClient, DateTimeOffset.UtcNow), policy)
                .AssignAggregateId()
                .WaitFor(new WaitForPipelineStatus(PipelineRunClient, recordService.GetAggregateId(), "Failed"), policy);
        }
    }
}


using Azure.Analytics.Synapse.Artifacts;
using Azure.Core;
using Azure.Identity;
using Azure.Storage.Files.DataLake;
using System;
using System.Collections.Generic;
using System.Text;

namespace FluidTestSynapse
{
    public abstract class TestExecutionBase
    {
        private Uri SynapseUri() => new Uri("https://XXXXXX.dev.azuresynapse.net");

        private Uri StorageUri() => new Uri("https://XXXXXX.dfs.core.windows.net");

        public ClientSecretCredential ClientCredentials => new ClientSecretCredential("TENANTIDHERE", "APPID_HERE", "SECRET HERE", new TokenCredentialOptions());

       public TokenCredential TokenCredentials => new ClientSecretCredential("TENANTIDHERE", "APPID_HERE", "SECRET HERE", new TokenCredentialOptions());

        public PipelineRunClient PipelineRunClient => new PipelineRunClient(SynapseUri(), ClientCredentials);

        public DataLakeServiceClient DataLakeClient => new DataLakeServiceClient(StorageUri(), TokenCredentials);

    }
}

```
