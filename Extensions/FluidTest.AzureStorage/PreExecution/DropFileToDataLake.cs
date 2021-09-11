using Azure.Storage.Files.DataLake;
using Marktek.Fluent.Testing.Engine.Interfaces;
using System.IO;
using System.Reflection;

namespace FluidTest.AzureStorage.PreExecution
{
    public class DropFileToDataLake : IPreExecution
    {
        private string filePath;
        private string fileName;
        private string containerName;
        private DataLakeServiceClient client;

        public DropFileToDataLake(string containerName, string filePath, string fileName, DataLakeServiceClient dataLakeClient)
        {
            this.filePath = filePath;
            this.fileName = fileName;
            this.containerName = containerName;
            this.client = dataLakeClient;
        }

        public void Execute()
        {
            var source = $"{ Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}/TestCases/TestData/{fileName}";

            var fileSystemClient = client.GetFileSystemClient(this.containerName);
            DataLakeDirectoryClient directoryClient = fileSystemClient.GetDirectoryClient(filePath);
            DataLakeFileClient fileClient = directoryClient.GetFileClient(fileName);

            using (FileStream stream = File.OpenRead(source))
            {
                fileClient.Upload(stream, new Azure.Storage.Files.DataLake.Models.DataLakeFileUploadOptions() { Close = true });
            }
        }
    }
}