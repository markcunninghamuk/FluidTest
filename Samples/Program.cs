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
                 .CreateRecord(new ActiveOrderConfiguration(service.AggregateId))
                 .ExecuteAction(new CustomExecutor())
                 .Delay(5000)
                 .ExecuteActionOnAggregate(new CustomExecutor())
                 .Delay(1000)
                 .If(DateTime.Now.Hour > 15, x => x.CreateRelatedRecord(new ActiveOrderConfiguration(Guid.NewGuid())))
                 .AssertAgainst(new MustBe())
                 .Cleanup(new Cleanup());
        }


    }
}