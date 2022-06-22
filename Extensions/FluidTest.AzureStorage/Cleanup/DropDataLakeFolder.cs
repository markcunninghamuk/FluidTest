using Azure.Storage.Files.DataLake;
using Marktek.Fluent.Testing.Engine.Interfaces;
using MarkTek.Fluent.Testing.RecordGeneration;
using System.Collections.Generic;

namespace FluidTest.AzureStorage.Cleanup
{
    public class DropDataLakeFolder : IRecordCleanup<string>
    {
        private string folderPath;
        private string containerName;
        private DataLakeServiceClient client;

        public DropDataLakeFolder(string containerName, string folderPath, DataLakeServiceClient client)
        {
            this.containerName = containerName;
            this.folderPath = folderPath;
            this.client = client;
        }

        public void Cleanup(List<Record<object, string>> records, string aggregateId)
        {
            var fileSystemClient = client.GetFileSystemClient(this.containerName);
            fileSystemClient.DeleteDirectory(this.folderPath);  
        }
    }
}
