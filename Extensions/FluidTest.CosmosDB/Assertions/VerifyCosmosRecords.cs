using FluidTest.CosmosDB.Validators;
using Marktek.Fluent.Testing.Engine;
using MarkTek.Fluent.Testing.RecordGeneration;
using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;

namespace FluidTest.CosmosDB.Assertions
{
    public class VerifyCosmosRecords : BaseValidator<string, IEnumerable<object>>
    {
        private int recordCountexpected;
        private CosmosClient client;
        private string databaseName;
        private string containerName;
        private Dictionary<string, string> mappingValues;

        public VerifyCosmosRecords(int recordCountexpected, Dictionary<string, string> mappingValues, CosmosClient client, string databaseName, string containerName)
        {
            this.recordCountexpected = recordCountexpected;
            this.databaseName = databaseName;
            this.containerName = containerName;
            this.client = client;
            this.mappingValues = mappingValues;
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
                new CosmosDbRecordCountShouldBe(this.recordCountexpected),
                new CosmosDbRecordValuesShouldBe(this.mappingValues)
            };
        }
    }
}
