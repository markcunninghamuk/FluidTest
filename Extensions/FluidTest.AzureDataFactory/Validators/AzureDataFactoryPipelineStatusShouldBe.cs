using FluentAssertions;
using MarkTek.Fluent.Testing.RecordGeneration;
using Microsoft.Azure.Management.DataFactory.Models;

namespace FluidTest.AzureSynapse.Validators
{
    public class AzureDataFactoryPipelineStatusShouldBe : ISpecificationValidator<PipelineRun>
    {
        private string status;

        public AzureDataFactoryPipelineStatusShouldBe(string status)
        {
            this.status = status;
        }
        public void Validate(PipelineRun item)
        {
            item.Status.Should().Be(this.status);
        }
    }
}
