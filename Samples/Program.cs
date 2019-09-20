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
             .CreateRecord(container.GetInstance<OrderConfiguration>())
             .CreateRelatedRecord(container.GetInstance<OrderConfiguration>())
             .If(DateTime.Now.Hour > 15, x => x.CreateRecord(container.GetInstance<OrderConfiguration>()));

        }

        internal class MyCustomConfig : IRelatedRecordCreator<OrderConfiguration, int>, IRecordCreator<OrderConfiguration, int>
        {
            public Record<OrderConfiguration, int> CreateRecord()
            {
                throw new NotImplementedException();
            }

            public Record<OrderConfiguration, Guid> CreateRecord(int id)
            {
                throw new NotImplementedException();
            }

            Record<OrderConfiguration, int> IRelatedRecordCreator<OrderConfiguration, int>.CreateRecord(int id)
            {
                throw new NotImplementedException();
            }
        }

    }
}