using Azure.Storage;
using Azure.Storage.Files.DataLake;
using Marktek.Fluent.Testing.Engine.Interfaces;

namespace FluidTest.AzureStorage.PreExecution
{
    public class CreateFolderOnDataLakeContainerIfNotExists : IPreExecution
    {     
        private string containerName;
        private DataLakeServiceClient client;
        private string folderPath;

        /// <summary>
        /// Creates a folder on a given container
        /// </summary>
        /// <param name="containerName"></param>
        /// <param name="folderPath"></param>
        /// <param name="dataLakeClient"></param>
        public CreateFolderOnDataLakeContainerIfNotExists(string containerName, string folderPath, DataLakeServiceClient dataLakeClient)
        {          
            this.containerName = containerName;
            client = dataLakeClient;
            this.folderPath = folderPath;
        }

        public void Execute()
        {         
            var fileSystemClient = client.GetFileSystemClient(this.containerName);
            var directoryClient = fileSystemClient.GetDirectoryClient(folderPath);

            if(!directoryClient.Exists())
            {
                fileSystemClient.CreateDirectory(this.folderPath);
            }            
        }
    }
}