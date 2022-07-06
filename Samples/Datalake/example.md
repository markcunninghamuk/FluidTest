## Intro

This example shows how you can create containers and folders and at the end you can cleanup the entities created during the test. This test shows how you can prepare the data, run the test and then finally cleanup at the end.

Remember to install the following Nuget Packages in your project

```cs
FluidTest
FluidTest.AzureStorage
```

```cs
using Azure.Storage.Files.DataLake;
using MarkTek.Fluent.Testing.RecordGeneration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluidTest.AzureStorage.PreExecution;
using FluidTest.AzureStorage.Assertions;
using FluidTest.AzureStorage.Cleanup;

namespace FluidTest.Samples
{
    [TestClass]
    public class DatalakeTests
    {
        private DataLakeServiceClient client;
        private IRecordService<string> recordService;

        [TestInitialize]
        public void Setup()
        {
            client  = new DataLakeServiceClient("YOUR CREDENTIALS HERE OR FROM ENVIRONMENT VARIABLE IDEALLY");
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
