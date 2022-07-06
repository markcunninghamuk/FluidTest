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


        public ClientSecretCredential ClientCredentials => new ClientSecretCredential(Environment.GetEnvironmentVariable("TenantId"), Environment.GetEnvironmentVariable("SpClientId"), Environment.GetEnvironmentVariable("SpClientSecret"), new TokenCredentialOptions());

        public TokenCredential TokenCredentials => new ClientSecretCredential(Environment.GetEnvironmentVariable("TenantId"), Environment.GetEnvironmentVariable("SpClientId"), Environment.GetEnvironmentVariable("SpClientSecret"), new TokenCredentialOptions());


        public DataLakeServiceClient DataLakeClient => new DataLakeServiceClient(StorageUri(), TokenCredentials);

        public Policy DefaultRetryPolicy => Policy
            .Handle<ExecutionWaitException>()
            .WaitAndRetry(120, retryAttempt => TimeSpan.FromSeconds(5));

        public CosmosClient CosmosClient => new CosmosClient(Environment.GetEnvironmentVariable("CosmosDbEndpoint"), Environment.GetEnvironmentVariable("CosmosDbKey"));

        [TestInitialize]
        public void Setup()
        {
            this.recordService = new RecordService<string>(string.Empty);
        }

    }
}
