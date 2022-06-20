using Azure.Storage.Files.DataLake;
using MarkTek.Fluent.Testing.RecordGeneration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluidTest.AzureStorage.PreExecution;
using FluidTest.AzureStorage.Assertions;
using FluidTest.AzureStorage.Cleanup;

namespace FluidTest.Samples
{
  //  [Ignore]
    [TestClass]
    public class DatalakeTests
    {
        private DataLakeServiceClient client;
        private IRecordService<string> recordService;

        [TestInitialize]
        public void Setup()
        {
            client  = new DataLakeServiceClient("DefaultEndpointsProtocol=https;AccountName=synapseintegrationtest;AccountKey=OjgQwtdavfkyknmumITeJc0PCO7kYpgXuV5N1j644JLaCDTMDo7hUpdzoiyi8SLEpdTTV4qKJGMEcom9F69Vkw==;EndpointSuffix=core.windows.net");
            this.recordService = new RecordService<string>(string.Empty);
        }

        [DataTestMethod]
        [DataRow("testcontainer1","RAW")]
        public void CreateContainerAndFolderAndCheckItExists(string containerName, string folderName)
        {
            recordService
                .PreExecutionAction(new CreateDataLakeContainerIfNotExists(containerName, client, Azure.Storage.Files.DataLake.Models.PublicAccessType.FileSystem))
                .PreExecutionAction(new CreateFolderOnDataLakeContainerIfNotExists(containerName, folderName, client))
                .AssertAgainst(new VerifyDataLakeFolderExist(client, containerName, folderName))
                .Cleanup(new DropDataLakeContainer(containerName, client)); 
        }
    }
}
