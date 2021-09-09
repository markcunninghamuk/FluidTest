using Azure.Search.Documents.Indexes.Models;
using FluentAssertions;
using MarkTek.Fluent.Testing.RecordGeneration;
using System.Linq;

namespace FluidTest.AzureSearch.Validators
{
    internal class AzureSearchIndexHasFields : ISpecificationValidator<SearchIndex>
    {
        private string fields;
        private string rootObject;

        public AzureSearchIndexHasFields(string fields, string rootObject)
        {
            this.fields = fields;
            this.rootObject = rootObject;
        }

        public void Validate(SearchIndex item)
        {
            this.fields.Split(',').ToList().ForEach(fieldName =>
            {
                if (string.IsNullOrWhiteSpace(rootObject))
                {
                    item.Fields.First(x => x.Name == fieldName).Should().NotBeNull();
                }
                else
                {
                    item.Fields.First(x => x.Name == this.rootObject).Fields.First(x => x.Name == fieldName).Should().NotBeNull();
                }
            });       
        }
    }
}