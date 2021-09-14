using FluentAssertions;
using MarkTek.Fluent.Testing.RecordGeneration;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

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

            JSchema schema = JSchema.Parse(sourceSchema);

            foreach (var document in item)
            {
                IList<string> messages;
                JObject model = JObject.Parse(document.ToString());

                bool valid = model.IsValid(schema, out messages);

                foreach (var validationError in messages)
                {
                    Console.WriteLine($"Schema validation failed with error {validationError}");
                }

                valid.Should().BeTrue();

            }
        }
    }
}
