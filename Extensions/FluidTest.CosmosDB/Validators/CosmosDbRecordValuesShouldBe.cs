using FluentAssertions;
using MarkTek.Fluent.Testing.RecordGeneration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FluidTest.CosmosDB.Validators
{
    public class CosmosDbRecordValuesShouldBe : ISpecificationValidator<IEnumerable<object>>
    {
        Dictionary<string, string> recordMappings;

        public CosmosDbRecordValuesShouldBe(Dictionary<string,string> values)
        {
            if (values == null)
            {
                throw new ArgumentException("Mapping values should be provided");
            }
            
            recordMappings = values;
        }

        public void Validate(IEnumerable<object> item)
        {
            foreach (var doc in item)
            {

            }
        }
    }
}
