using MarkTek.Fluent.Testing.RecordGeneration;
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

            var service = new RecordService<Guid>();

            service
             .CreateRecord(container.GetInstance<ActiveOrderConfiguration>())
             .CreateRelatedRecord(container.GetInstance<ActiveOrderConfiguration>())
             .CreateRecord(new CustomOrder())
             .If(DateTime.Now.Hour > 15, x => x.CreateRecord(container.GetInstance<ActiveOrderConfiguration>()));
           
        }

        internal class MyCustomConfig : IRelatedRecordCreator<ActiveOrderConfiguration, int>, IRecordCreator<ActiveOrderConfiguration, int>
        {
            public Record<ActiveOrderConfiguration, int> CreateRecord()
            {
                throw new NotImplementedException();
            }

            public Record<ActiveOrderConfiguration, Guid> CreateRecord(int id)
            {
                throw new NotImplementedException();
            }

            Record<ActiveOrderConfiguration, int> IRelatedRecordCreator<ActiveOrderConfiguration, int>.CreateRecord(int id)
            {
                throw new NotImplementedException();
            }
        }

    }
}