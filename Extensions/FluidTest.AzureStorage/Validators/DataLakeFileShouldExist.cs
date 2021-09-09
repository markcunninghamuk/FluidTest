using Azure.Storage.Files.DataLake;
using FluentAssertions;
using MarkTek.Fluent.Testing.RecordGeneration;

namespace FluidTest.AzureStorage.Validators
{
    internal class DataLakeFileShouldExist : ISpecificationValidator<DataLakeFileClient>
    {
        public void Validate(DataLakeFileClient item)
        {
            item.Exists().Value.Should().BeTrue();
        }
    }
}