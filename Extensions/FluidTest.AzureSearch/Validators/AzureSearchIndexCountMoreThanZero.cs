using Azure.Search.Documents.Models;
using FluentAssertions;
using MarkTek.Fluent.Testing.RecordGeneration;

namespace FluidTest.AzureSearch.Validators
{
    internal class AzureSearchIndexCountMoreThanZero : ISpecificationValidator<SearchResults<object>>
    {
        public void Validate(SearchResults<object> item)
        {
            foreach (var doc in item.GetResults())
            {
                doc.Document.Should().NotBeNull();
            }
        }
    }
}