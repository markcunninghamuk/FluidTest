using Marktek.Fluent.Testing.Engine.Sample.ExportApplications.Cleanup;
using MarkTek.Fluent.Testing.RecordGeneration;
using MarkTek.Fluent.Testing.Sample.Specifications;
using MarkTek.Fluent.Testing.Sample.Specifications.Config;
using StructureMap;
using System;

namespace Marktek.Fluent.Testing.Engine.Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = new Container(_ =>
            {
                _.Scan(x =>
                {
                    x.TheCallingAssembly();
                    x.WithDefaultConventions();
                });
            });

            var service = new RecordService<Guid>(Guid.NewGuid());
            
            service
                 .CreateRecord(container.GetInstance<ActiveOrderConfiguration>())
                 .If(DateTime.Now.Hour > 15, x => x.CreateRelatedRecord(container.GetInstance<ActiveOrderConfiguration>()))
                 .AssertAgainst(new System.Collections.Generic.List<ISpecifcation>
                 {
                     new MustBeOpenSpecification(service.AggregateId)
                 })
                 .Cleanup(new Cleanup(service.AggregateId));
        }


    }
}