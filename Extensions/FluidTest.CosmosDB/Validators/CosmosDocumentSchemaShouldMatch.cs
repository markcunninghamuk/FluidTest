using FluentAssertions;
using MarkTek.Fluent.Testing.RecordGeneration;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using NJsonSchema;
using Newtonsoft.Json.Linq;

namespace FluidTest.CosmosDB.Validators
{
    public class CosmosDocumentSchemaShouldMatch : ISpecificationValidator<IEnumerable<object>>
    {
        private string schemaFileName;

        public CosmosDocumentSchemaShouldMatch(string schema)
        {
            this.schemaFileName = schema;
        }

        public void Validate(IEnumerable<object> item)
        {
            var sourceSchema = $"{ Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}/TestCases/Schema/{schemaFileName}";

            var schemaRaw = File.ReadAllText(sourceSchema);

            var schema = JsonSchema.FromSampleJson(schemaRaw);
           
            foreach (var document in item)
            {
               // JObject model = JObject.Parse(document.ToString());
                var errors = schema.Validate(document.ToString());                
                bool valid = errors.Count == 0;
                valid.Should().BeTrue();
            }
        }
    }
}