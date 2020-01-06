using System;
using MarkTek.Fluent.Testing.RecordGeneration;

namespace Marktek.Fluent.Testing.Engine.Sample
{
    internal class CustomOrder : IRecordCreator<object, Guid>
    {
        public Record<object, Guid> CreateRecord()
        {
            return new Record<object, Guid>(new { }, Guid.NewGuid());
        }
    }
}