using Azure.Search.Documents.Indexes;
using Azure.Search.Documents.Indexes.Models;
using FluidTest.AzureSearch.Validators;
using Marktek.Fluent.Testing.Engine;
using MarkTek.Fluent.Testing.RecordGeneration;
using System.Collections.Generic;

namespace FluidTest.AzureSearch.Assertions
{
    internal class VerifyAzureSearchFieldsExist : BaseValidator<string, SearchIndex>
    {
        private string fields;
        private SearchIndexClient indexClient;
        private string indexName;
        private string rootObject;

        public VerifyAzureSearchFieldsExist(string fields, string indexName, SearchIndexClient indexClient, string rootObject)
        {
            this.fields = fields;
            this.indexName = indexName;
            this.indexClient = indexClient;
            this.rootObject = rootObject;
        }

        public override SearchIndex GetRecord(string id)
        {
            return this.indexClient.GetIndex(indexName);
        }

        public override List<ISpecificationValidator<SearchIndex>> GetValidators()
        {
            return new List<ISpecificationValidator<SearchIndex>> 
            { 
                new AzureSearchIndexHasFields(this.fields, this.rootObject)
            };
        }
    }
}