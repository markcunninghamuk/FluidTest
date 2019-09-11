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

            var service = new RecordService<Guid>(); //My Database uses guids, if yours uses int, use int, etc, 
            //you can also override the methods and implement your own
                        
            service  
             .CreateRecord(container.GetInstance<OrderConfiguration>())
             .CreateRelatedRecord(container.GetInstance<OrderConfiguration>())
             .AssertAgainst(container.GetInstance<MustBeOpenSpecification>());

        }
    }
}
