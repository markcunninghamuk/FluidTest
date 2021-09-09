using Azure.Search.Documents.Indexes.Models;
using FluentAssertions;
using MarkTek.Fluent.Testing.RecordGeneration;

namespace FluidTest.AzureSearch.Validators
{
    internal class AzureSearchIndexShouldExist : ISpecificationValidator<SearchIndex>
    {
        private string indexName;

        public AzureSearchIndexShouldExist(string indexName)
        {
            this.indexName = indexName;
        }
        public void Validate(SearchIndex item)
        {
            item.Name.Should().Be(this.indexName);
        }
    }
}