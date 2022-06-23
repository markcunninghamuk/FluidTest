using Azure.Storage.Blobs;
using FluidTest.AzureStorage.Validators;
using Marktek.Fluent.Testing.Engine;
using MarkTek.Fluent.Testing.RecordGeneration;
using System.Collections.Generic;

namespace FluidTest.AzureStorage.Assertions
{
    public class VerifyBlobFileExist : BaseValidator<string, BlobClient>
    {
        private readonly string fileName;
        private readonly string containerName;
        private readonly BlobServiceClient client;

        public VerifyBlobFileExist(string containerName, string fileName, BlobServiceClient blobServiceClient)
        {
            this.fileName = fileName;
            this.containerName = containerName;
            client = blobServiceClient;
        }

        public override BlobClient GetRecord(string id)
        {
            return client.GetBlobContainerClient(containerName).GetBlobClient(fileName);
        }

        public override List<ISpecificationValidator<BlobClient>> GetValidators()
        {
            return new List<ISpecificationValidator<BlobClient>>
            {
                new BlobFileShouldExist()
            };
        }
    }
}
