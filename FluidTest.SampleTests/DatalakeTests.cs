using Azure.Storage.Files.DataLake;
using MarkTek.Fluent.Testing.RecordGeneration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluidTest.AzureStorage.PreExecution;
using FluidTest.AzureStorage.Assertions;
using FluidTest.AzureStorage.Cleanup;
using FluidTest.SampleTests.Base;

namespace FluidTest.Samples
{
  //  [Ignore]
    [TestClass]
    public class DatalakeTests: TestExecutionBase
    {

        [DataTestMethod]
        [DataRow("testcontainer1","RAW")]
        public void CreateContainerAndFolderAndCheckItExists(string containerName, string folderName)
        {
            recordService
                .PreExecutionAction(new CreateDataLakeContainerIfNotExists(containerName, DataLakeClient, Azure.Storage.Files.DataLake.Models.PublicAccessType.FileSystem))
                .PreExecutionAction(new CreateFolderOnDataLakeContainerIfNotExists(containerName, folderName, DataLakeClient))
                .AssertAgainst(new VerifyDataLakeFolderExist(DataLakeClient, containerName, folderName))
                .Cleanup(new DropDataLakeContainer(containerName, DataLakeClient)); 
        }
    }
}
