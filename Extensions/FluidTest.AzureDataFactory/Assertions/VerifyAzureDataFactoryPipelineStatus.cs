using FluidTest.AzureSynapse.Validators;
using Marktek.Fluent.Testing.Engine;
using MarkTek.Fluent.Testing.RecordGeneration;
using System.Collections.Generic;
using Microsoft.Azure.Management.DataFactory.Models;
using Microsoft.Azure.Management.DataFactory;

namespace FluidTest.AzureSynapse.Assertions
{
    public class VerifyAzureDataFactoryPipelineStatus : BaseValidator<string, PipelineRun>
    {
        private string status;
        private DataFactoryManagementClient pipelineRunClient;
        private string dataFactoryInstanceName;
        private string resourceGroupName;

        public VerifyAzureDataFactoryPipelineStatus(string status, DataFactoryManagementClient client, string resourceGroupName, string dataFactoryInstanceName)
        {
            this.status = status;
            this.pipelineRunClient = client;
            this.resourceGroupName = resourceGroupName;
            this.dataFactoryInstanceName = dataFactoryInstanceName;
        }

        public override PipelineRun GetRecord(string id)
        {
            return this.pipelineRunClient.PipelineRuns.Get(this.resourceGroupName, this.dataFactoryInstanceName, id);
        }

        public override List<ISpecificationValidator<PipelineRun>> GetValidators()
        {
            return new List<ISpecificationValidator<PipelineRun>>
            {
                new AzureDataFactoryPipelineStatusShouldBe(this.status)
            };
        }
    }
}
