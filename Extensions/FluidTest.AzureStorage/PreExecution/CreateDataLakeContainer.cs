using Azure.Storage;
using Azure.Storage.Files.DataLake;
using Azure.Storage.Files.DataLake.Models;
using Marktek.Fluent.Testing.Engine.Interfaces;

namespace FluidTest.AzureStorage.PreExecution
{
    public class CreateDataLakeContainer : IPreExecution
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
        public CreateDataLakeContainer(string containerName, DataLakeServiceClient dataLakeClient, PublicAccessType accessType)
        {          
            this.containerName = containerName;
            client = dataLakeClient;
            this.datalakeAccess = accessType;
        }

        public void Execute()
        {
            client.CreateFileSystem(containerName, datalakeAccess);
        }
    }
}