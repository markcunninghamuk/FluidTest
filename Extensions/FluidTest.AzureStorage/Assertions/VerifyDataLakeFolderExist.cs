using Azure.Storage.Files.DataLake;
using FluidTest.AzureStorage.Validators;
using Marktek.Fluent.Testing.Engine;
using MarkTek.Fluent.Testing.RecordGeneration;
using System.Collections.Generic;

namespace FluidTest.AzureStorage.Assertions
{
    internal class VerifyDataLakeFolderExist : BaseValidator<string, DataLakeDirectoryClient>
    {
        private DataLakeServiceClient client;
        private string containerName;
        private string folderPath;

        public VerifyDataLakeFolderExist(DataLakeServiceClient dataLakeCientService, string containerName, string folderPath)
        {
            this.client = dataLakeCientService;
            this.containerName = containerName;
            this.folderPath = folderPath;
        }

        public override DataLakeDirectoryClient GetRecord(string id)
        {
            var fileSystemClient = client.GetFileSystemClient(this.containerName);
            DataLakeDirectoryClient directoryClient = fileSystemClient.GetDirectoryClient(this.folderPath);
            return directoryClient;
        }

        public override List<ISpecificationValidator<DataLakeDirectoryClient>> GetValidators()
        {
            return new List<ISpecificationValidator<DataLakeDirectoryClient>>
            {
                new DataLakeFolderShouldExist()
            };
        }
    }
}