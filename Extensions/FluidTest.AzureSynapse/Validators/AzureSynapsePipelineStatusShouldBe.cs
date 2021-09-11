using Azure.Analytics.Synapse.Artifacts.Models;
using FluentAssertions;
using MarkTek.Fluent.Testing.RecordGeneration;

namespace FluidTest.AzureSynapse.Validators
{
    public class AzureSynapsePipelineStatusShouldBe : ISpecificationValidator<PipelineRun>
    {
        private string status;

        public AzureSynapsePipelineStatusShouldBe(string status)
        {
            this.status = status;
        }
        public void Validate(PipelineRun item)
        {
            item.Status.Should().Be(this.status);
        }
    }
}
