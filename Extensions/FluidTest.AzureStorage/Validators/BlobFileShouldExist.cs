using Azure.Storage.Blobs;
using FluentAssertions;
using MarkTek.Fluent.Testing.RecordGeneration;

namespace FluidTest.AzureStorage.Validators
{
    public class BlobFileShouldExist : ISpecificationValidator<BlobClient>
    {
        public void Validate(BlobClient item)
        {
            item.Exists().Value.Should().BeTrue();
        }
    }
}