using Marktek.Fluent.Testing.Engine.Exceptions;
using Marktek.Fluent.Testing.Engine.Interfaces;
using Azure.Storage.Files.DataLake;

namespace FluidTest.AzureStorage.WaitActions
{
    public class WaitForFolderCreation : IWaitableAction
    {
        private DataLakeServiceClient client;
        private string folderPath;
        private string containerName;

        public WaitForFolderCreation(DataLakeServiceClient dataLakeClient, string containerName, string folderPath)
        {
            this.client = dataLakeClient;
            this.folderPath = folderPath;
            this.containerName = containerName;
        }

        public void Execute()
        {
            var fsc = client.GetFileSystemClient(containerName);
            var dir = fsc.GetDirectoryClient(folderPath);
            bool exists = dir.Exists().Value;

            if (!exists)
                throw new ExecutionWaitException($"Folder Path: {folderPath} does not exist. Backing off and retrying");

        }
    }
}