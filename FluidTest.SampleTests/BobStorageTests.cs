using Azure.Storage.Blobs;
using FluidTest.AzureStorage.Assertions;
using FluidTest.AzureStorage.PreExecution;
using MarkTek.Fluent.Testing.RecordGeneration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluidTest.SampleTests
{
    
    [TestClass]
    public class BobStorageTests
    {
        private BlobServiceClient client;
        private IRecordService<string> recordService;

        [TestInitialize]
        public void Setup()
        {
            this.client = new BlobServiceClient("XXXXXXXXXXXXXXXXXXXXXXXX");
            this.recordService = new RecordService<string>(string.Empty);
        }

        [DataTestMethod]
        [DataRow("data", "temp.txt")]
        public void BlobFile_Should_Exist(string containerName, string fileName)
        {
            recordService
                .PreExecutionAction(new DropFileToBlobContainer(containerName, fileName, this.client))
                .AssertAgainst(new VerifyBlobFileExist(containerName, fileName, this.client)); 
        }
    }

}

