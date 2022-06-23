using Azure.Storage.Blobs;
using Marktek.Fluent.Testing.Engine.Interfaces;
using System.IO;
using System.Reflection;

namespace FluidTest.AzureStorage.PreExecution
{
    public class DropFileToBlobContainer : IPreExecution
    {
        private readonly string fileName;
        private readonly string containerName;
        private readonly BlobServiceClient client;

        public DropFileToBlobContainer(string containerName, string fileName, BlobServiceClient blobServiceClient)
        {
            this.fileName = fileName;
            this.containerName = containerName;
            client = blobServiceClient;
        }

        public void Execute()
        {
            var source = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}/TestCases/TestData/{fileName}";

            var containerClient = client.GetBlobContainerClient(containerName);
            containerClient.CreateIfNotExists();

            using (FileStream stream = File.OpenRead(source))
            {
                containerClient.UploadBlob(fileName, stream);
            }
        }
    }
}