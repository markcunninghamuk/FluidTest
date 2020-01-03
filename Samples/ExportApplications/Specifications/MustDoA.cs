using MarkTek.Fluent.Testing.ExportApplications.Model;
using MarkTek.Fluent.Testing.RecordGeneration;
using System;

namespace MarkTek.Fluent.Testing.Sample.Specifications
{
    internal class MustDoA : ISpecificationValidator<Order>
    {
        public void Validate(Order item)
        {
            Console.Write("A isvalud");
        }
    }
}