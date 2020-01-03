using MarkTek.Fluent.Testing.RecordGeneration;
using System;
using System.IO;

namespace MarkTek.Fluent.Testing.Sample.Specifications
{
    public class MustBeOpenSpecification : ISpecifcation
    {
        private Guid aggregateId;

        public MustBeOpenSpecification(Guid aggregateId)
        {
            this.aggregateId = aggregateId;
        }

        public void Validate()
        {
            var res = File.ReadAllText("C:\\Test\\test.txt");
            
            if (!res.Contains("Creating Order"))
            {
                throw new System.Exception("Could not find the expected string");
            }
        }
    }
}
