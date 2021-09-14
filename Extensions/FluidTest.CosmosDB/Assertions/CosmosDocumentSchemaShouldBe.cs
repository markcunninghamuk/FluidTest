using FluidTest.CosmosDB.Validators;
using Marktek.Fluent.Testing.Engine;
using MarkTek.Fluent.Testing.RecordGeneration;
using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FluidTest.CosmosDB.Assertions
{

    public class CosmosDocumentSchemaShouldBeValid : BaseValidator<string, IEnumerable<object>>
    {
        private CosmosClient cosmosClient;
        private string schema;
        private string containerName;
        private string databaseName;

        public CosmosDocumentSchemaShouldBeValid(CosmosClient client, string schemaFile, string containerName, string databaseName)
        {
            this.cosmosClient = client;
            this.schema = schemaFile;
            this.containerName = containerName;
            this.databaseName = databaseName;
        }

        public override IEnumerable<object> GetRecord(string id)
        {
            var document = this.cosmosClient.GetContainer(databaseName, containerName).GetItemQueryIterator<object>("select * from c");

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
                new CosmosDocumentSchemaShouldMatch(this.schema)
            };
        }
    }
}
