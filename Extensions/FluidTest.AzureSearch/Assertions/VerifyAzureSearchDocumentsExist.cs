using Azure.Search.Documents;
using Azure.Search.Documents.Models;
using FluidTest.AzureSearch.Validators;
using Marktek.Fluent.Testing.Engine;
using MarkTek.Fluent.Testing.RecordGeneration;
using System.Collections.Generic;

namespace FluidTest.AzureSearch.Assertions
{
    public class VerifyAzureSearchDocumentsExist : BaseValidator<string, SearchResults<object>>
    {
        private SearchClient searchClient;

        public VerifyAzureSearchDocumentsExist(SearchClient searchClient)
        {
            this.searchClient = searchClient;
        }

        public override SearchResults<object> GetRecord(string id)
        {
            return searchClient.Search<object>(string.Empty);
        }

        public override List<ISpecificationValidator<SearchResults<object>>> GetValidators()
        {
            return new List<ISpecificationValidator<SearchResults<object>>>
            {
                new AzureSearchIndexCountMoreThanZero(),
                new AzureSearchIndexScoreShouldBe(1)
            };
        }
    }
}