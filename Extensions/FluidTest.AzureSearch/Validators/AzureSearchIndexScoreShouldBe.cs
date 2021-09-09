using Azure.Search.Documents.Models;
using FluentAssertions;
using MarkTek.Fluent.Testing.RecordGeneration;

namespace FluidTest.AzureSearch.Validators
{
    internal class AzureSearchIndexScoreShouldBe : ISpecificationValidator<SearchResults<object>>
    {
        private int expectedScore;

        public AzureSearchIndexScoreShouldBe(int expectedScore)
        {
            this.expectedScore = expectedScore;
        }
        public void Validate(SearchResults<object> item)
        {
            foreach (var doc in item.GetResults())
            {
                doc.Score.Should().Be(this.expectedScore);
            }
        }
    }
}