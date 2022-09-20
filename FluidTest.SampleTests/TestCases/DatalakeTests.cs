using Azure.Storage.Files.DataLake;
using MarkTek.Fluent.Testing.RecordGeneration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluidTest.AzureStorage.PreExecution;
using FluidTest.AzureStorage.Assertions;
using FluidTest.AzureStorage.Cleanup;
using FluidTest.SampleTests.Base;

namespace FluidTest.Samples.TestCases
{
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
                .Cleanup(new DropDataLakeFolder(containerName, folderName, DataLakeClient))
                .Cleanup(new DropDataLakeContainer(containerName, DataLakeClient)); 
        }

        [DataTestMethod]
        [DataRow("testcontainer1", "RAW", "xmlfull.zip")]
        public void DropFileToContainer(string containerName, string folderName, string fileName)
        {
            recordService
                .PreExecutionAction(new CreateDataLakeContainerIfNotExists(containerName, DataLakeClient, Azure.Storage.Files.DataLake.Models.PublicAccessType.FileSystem))
                .PreExecutionAction(new CreateFolderOnDataLakeContainerIfNotExists(containerName, folderName, DataLakeClient))
                .PreExecutionAction(new DropFileToDataLake(containerName, folderName, fileName, DataLakeClient))
                .AssertAgainst(new VerifyDataLakeFolderExist(DataLakeClient, containerName, folderName));
        }
    }
}
