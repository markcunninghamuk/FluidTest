using Azure.Storage.Files.DataLake;
using FluidTest.AzureStorage.Validators;
using Marktek.Fluent.Testing.Engine;
using MarkTek.Fluent.Testing.RecordGeneration;
using System.Collections.Generic;

namespace FluidTest.AzureStorage.Assertions
{
    public class VerifyDataLakeFileExist : BaseValidator<string, DataLakeFileClient>
    {
        private readonly DataLakeServiceClient client;
        private readonly string containerName;
        private readonly string folderPath;
        private readonly string fileName;

        public VerifyDataLakeFileExist(DataLakeServiceClient dataLakeCientService, string containerName, string folderPath, string fileName)
        {
            this.client = dataLakeCientService;
            this.containerName = containerName;
            this.folderPath = folderPath;
            this.fileName = fileName;
        }

        public override DataLakeFileClient GetRecord(string id)
        {
            var fileSystemClient = client.GetFileSystemClient(containerName);
            var directoryClient = fileSystemClient.GetDirectoryClient(folderPath);
            var fileClient = directoryClient.GetFileClient(fileName);
            return fileClient;
        }

        public override List<ISpecificationValidator<DataLakeFileClient>> GetValidators()
        {
            return new List<ISpecificationValidator<DataLakeFileClient>>
            {
                new DataLakeFileShouldExist()
            };
        }
    }
}