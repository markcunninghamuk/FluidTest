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
                 .PreExecutionAction(new PreOperation())
                 .CreateRecord(new ActiveOrderConfiguration(service.AggregateId))
                 .CreatedRelatedRecord(new ChildRecordPassedIn())
                 .AssignAggregateId()
                 .ExecuteAction(new CustomExecutor(), false)
                 .WaitFor(new WaitForExample())
                 .ExecuteAction(new CustomExecutor(), true)
                 .If(DateTime.Now.Hour > 15, x => x.CreateRelatedRecord(new ActiveOrderConfiguration(Guid.NewGuid())))
                 .AssertAgainst(new MustBeCancelled())
                 .Cleanup(new Cleanup());          

        }
    }
}