using Azure.Storage.Files.DataLake;
using FluentAssertions;
using MarkTek.Fluent.Testing.RecordGeneration;

namespace FluidTest.AzureStorage.Validators
{
    internal class DataLakeFolderShouldExist : ISpecificationValidator<DataLakeDirectoryClient>
    {
        public void Validate(DataLakeDirectoryClient item)
        {
            item.Exists().Value.Should().BeTrue();
        }
    }
}