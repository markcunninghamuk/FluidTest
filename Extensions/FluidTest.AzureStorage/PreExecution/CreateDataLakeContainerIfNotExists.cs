using Azure.Storage;
using Azure.Storage.Files.DataLake;
using Azure.Storage.Files.DataLake.Models;
using Marktek.Fluent.Testing.Engine.Interfaces;

namespace FluidTest.AzureStorage.PreExecution
{
    public class CreateDataLakeContainerIfNotExists : IPreExecution
    {     
        private string containerName;
        private DataLakeServiceClient client;
        private PublicAccessType datalakeAccess;

        /// <summary>
        /// Creates a folder on a given container
        /// </summary>
        /// <param name="containerName"></param>
        /// <param name="folderPath"></param>
        /// <param name="dataLakeClient"></param>
        public CreateDataLakeContainerIfNotExists(string containerName, DataLakeServiceClient dataLakeClient, PublicAccessType accessType)
        {          
            this.containerName = containerName;
            client = dataLakeClient;
            this.datalakeAccess = accessType;
        }

        public void Execute()
        {
            var container = client.GetFileSystemClient(containerName);

            if(!container.Exists())
            {
                client.CreateFileSystem(containerName, datalakeAccess);
            }            
        }
    }
}