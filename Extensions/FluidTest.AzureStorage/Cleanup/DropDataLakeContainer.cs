using Azure.Storage.Files.DataLake;
using Marktek.Fluent.Testing.Engine.Interfaces;
using MarkTek.Fluent.Testing.RecordGeneration;
using System.Collections.Generic;

namespace FluidTest.AzureStorage.Cleanup
{
    public class DropDataLakeContainer : IRecordCleanup<string>
    {
        private string containerName;
        private DataLakeServiceClient client;

        public DropDataLakeContainer(string containerName, DataLakeServiceClient client)
        {
            this.containerName = containerName;
            this.client = client;
        }

        public void Cleanup(List<Record<object, string>> records, string aggregateId)
        {
            client.DeleteFileSystem(this.containerName);
        }
    }
}
