using Azure.Storage;
using Azure.Storage.Files.DataLake;
using Marktek.Fluent.Testing.Engine.Interfaces;

namespace FluidTest.AzureStorage.PreExecution
{
    public class CreateFolderOnDataLakeContainer : IPreExecution
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
        public CreateFolderOnDataLakeContainer(string containerName, string folderPath, DataLakeServiceClient dataLakeClient)
        {          
            this.containerName = containerName;
            client = dataLakeClient;
            this.folderPath = folderPath;
        }

        public void Execute()
        {         
            var fileSystemClient = client.GetFileSystemClient(this.containerName);
            fileSystemClient.CreateDirectory(this.folderPath);
        }
    }
}