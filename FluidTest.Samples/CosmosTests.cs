using FluidTest.CosmosDB.Assertions;
using MarkTek.Fluent.Testing.RecordGeneration;
using Microsoft.Azure.Cosmos;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluidTest.Samples
{
    [TestClass]
    public class CosmosTests
    {
        private CosmosClient client;
        private IRecordService<string> recordService;

        [TestInitialize]
        public void Setup()
        {
            this.client = new CosmosClient("XXXXXXXXXXXXXXXXXXXXXXXX");
            this.recordService = new RecordService<string>(string.Empty);
        }

        [DataTestMethod]
        [DataRow("unknown")]
        public void Container_Should_Exist(string containerName)
        {
            recordService
                .AssertAgainst(new CosmosContainerShouldExist(this.client, containerName, "TrfPublishedSet"));
        }
    }
}
