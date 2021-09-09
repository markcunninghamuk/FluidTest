using MarkTek.Fluent.Testing.RecordGeneration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Marktek.Fluent.Testing.Engine.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TID"></typeparam>
    public interface IRecordCleanup<TID>
    {
        /// <summary>
        /// Cleans up all resources, provides all the records and id's created during the session
        /// </summary>
        /// <param name="records"></param>
        /// <param name="aggregateId"></param>
        void Cleanup(List<Record<object, TID>> records, TID aggregateId);
    }
}