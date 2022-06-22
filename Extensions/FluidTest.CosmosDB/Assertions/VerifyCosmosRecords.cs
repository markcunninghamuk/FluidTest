using FluidTest.CosmosDB.Validators;
using Marktek.Fluent.Testing.Engine;
using MarkTek.Fluent.Testing.RecordGeneration;
using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;

namespace FluidTest.CosmosDB.Assertions
{
    class VerifyCosmosRecords : BaseValidator<string, IEnumerable<object>>
    {
        private int recordCountexpected;
        private CosmosClient client;
        private string databaseName;
        private string containerName;

        public VerifyCosmosRecords(int recordCountexpected, CosmosClient client, string databaseName, string containerName, int expectedCount)
        {
            this.recordCountexpected = recordCountexpected;
            this.databaseName = databaseName;
            this.containerName = containerName;
            this.client = client;
        }

        public override IEnumerable<object> GetRecord(string id)
        {
            var document = this.client.GetContainer(databaseName, containerName).GetItemQueryIterator<object>("select * from c");

            List<object> docs = new List<object>();

            while (document.HasMoreResults)
            {
                var response = document.ReadNextAsync().GetAwaiter().GetResult();
                docs.AddRange(response.Resource);
            }

            return docs;

        }
        public override List<ISpecificationValidator<IEnumerable<object>>> GetValidators()
        {
            return new List<ISpecificationValidator<IEnumerable<object>>>
            {
                new CosmosDbRecordCountShouldBe(this.recordCountexpected)
            };
        }
    }
}
