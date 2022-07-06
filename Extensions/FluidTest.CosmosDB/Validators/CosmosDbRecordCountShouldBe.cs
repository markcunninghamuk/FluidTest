using FluentAssertions;
using MarkTek.Fluent.Testing.RecordGeneration;
using System.Collections.Generic;
using System.Linq;

namespace FluidTest.CosmosDB.Validators
{
    public class CosmosDbRecordCountShouldBe : ISpecificationValidator<IEnumerable<object>>
    {
        private int recordCountexpected;

        public CosmosDbRecordCountShouldBe(int recordCountexpected)
        {
            this.recordCountexpected = recordCountexpected;
        }

        public void Validate(IEnumerable<object> item)
        {
            item.Count().Should().Be(this.recordCountexpected);
        }
    }
}
