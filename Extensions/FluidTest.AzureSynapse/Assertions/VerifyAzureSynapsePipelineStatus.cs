using Azure.Analytics.Synapse.Artifacts;
using Azure.Analytics.Synapse.Artifacts.Models;
using FluidTest.AzureSynapse.Validators;
using Marktek.Fluent.Testing.Engine;
using MarkTek.Fluent.Testing.RecordGeneration;
using System.Collections.Generic;

namespace FluidTest.AzureSynapse.Assertions
{
    public class VerifyAzureSynapsePipelineStatus : BaseValidator<string, PipelineRun>
    {
        private string status;
        private PipelineRunClient pipelineRunClient;

        public VerifyAzureSynapsePipelineStatus(string status, PipelineRunClient client)
        {
            this.status = status;
            this.pipelineRunClient = client;
        }

        public override PipelineRun GetRecord(string id)
        {
            return this.pipelineRunClient.GetPipelineRun(id);
        }

        public override List<ISpecificationValidator<PipelineRun>> GetValidators()
        {
            return new List<ISpecificationValidator<PipelineRun>>
            {
                new AzureSynapsePipelineStatusShouldBe(this.status)
            };
        }
    }
}
