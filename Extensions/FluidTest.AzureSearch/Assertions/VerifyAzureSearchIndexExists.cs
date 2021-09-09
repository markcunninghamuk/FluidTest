using Azure.Search.Documents.Indexes;
using Azure.Search.Documents.Indexes.Models;
using FluidTest.AzureSearch.Validators;
using Marktek.Fluent.Testing.Engine;
using MarkTek.Fluent.Testing.RecordGeneration;
using System.Collections.Generic;

namespace FluidTest.AzureSearch.Assertions
{
    public class VerifyAzureSearchIndexExists : BaseValidator<string, SearchIndex>
    {
        private string indexName;
        private SearchIndexClient indexClient;

        public VerifyAzureSearchIndexExists(string indexName, SearchIndexClient client)
        {
            this.indexName = indexName;
            this.indexClient = client;
        }

        public override SearchIndex GetRecord(string id)
        {
            return this.indexClient.GetIndex(this.indexName);
        }

        public override List<ISpecificationValidator<SearchIndex>> GetValidators()
        {
            return new List<ISpecificationValidator<SearchIndex>>
            {
                new AzureSearchIndexShouldExist(this.indexName)
            };
        }
    }
}