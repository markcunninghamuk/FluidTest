
## Intro

This sample shows how to create a database and a container, then it creates documents before cleaning up.

Remember to install the following Nuget Packages in your project

```cs
FluidTest
FluidTest.AzureCosmosDB
```

```cs

namespace FluidTest.Samples
{   
    [TestClass]
    public class CosmosTests
    {
        private CosmosClient client;
      
        [TestInitialize]
        public void Setup()
        {
            this.client = new CosmosClient("YOUR CONNECTION STRING FROM ENVIRONMENT VARIABLE");
        }

        //Check a container exists in a database
        [DataTestMethod]
        [DataRow("myContainerName","myDatabaseName")]
        public void Container_Should_Exist(string containerName, string databaseName)
        {
            var recordService = new RecordService<string>(String.Empty);
            recordService
                .AssertAgainst(new CosmosContainerShouldExist(this.client, containerName, databaseName));
        }

        //Create a database and a container and clean it up at the end
        [DataTestMethod]
        [DataRow("myContainerName", "myDatabaseName",4000)]
        public void Create_Database_And_Container_Then_Cleanup(string containerName, string databaseName, int throughput)
        {
            var dynamicRecord = new { id = Guid.NewGuid().ToString(), name = "test" };

            var recordService = new RecordService<string>(String.Empty);

            recordService
               .PreExecutionAction(new CreateCosmosDatabaseIfNotExists(client, databaseName, ThroughputProperties.CreateAutoscaleThroughput(4000)))
               .PreExecutionAction(new CreateCosmosContainerIfNotExists(client, databaseName, new ContainerProperties { Id = containerName, PartitionKeyPath = "/id" }, 4000))
               .CreateRecord(new UpsertCosmosDocument<dynamic>(client, databaseName, containerName, dynamicRecord, new PartitionKey(dynamicRecord.id)))
               .AssertAgainst(new CosmosContainerShouldExist(this.client, containerName, databaseName))
               .Cleanup(new DropCosmosDatabase(databaseName,client));
        }

    }
}