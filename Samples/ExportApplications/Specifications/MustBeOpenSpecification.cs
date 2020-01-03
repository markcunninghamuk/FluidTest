using MarkTek.Fluent.Testing.ExportApplications.Model;
using MarkTek.Fluent.Testing.RecordGeneration;
using System;
using System.Collections.Generic;
using System.IO;

namespace MarkTek.Fluent.Testing.Sample.Specifications
{
    public class MustBeOpenSpecification : ISpecifcation<Guid, Order>
    {
        public List<ISpecificationValidator<Order>> GetValidators()
        {
            return new List<ISpecificationValidator<Order>>
           {
               new MustDoA(),
                new MustDoB()
           };
        }

        public void Validate(Guid aggregateId)
        {
            var x = GetValidators();
            x.ForEach(vv => vv.Validate(new Order()));

            var res = File.ReadAllText("C:\\Test\\test.txt");

            if (!res.Contains("Creating Order"))
            {
                throw new System.Exception("Could not find the expected string");
            }
        }
    }
}
