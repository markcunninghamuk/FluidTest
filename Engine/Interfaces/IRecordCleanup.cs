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
        void Cleanup(Dictionary<TID, object> records);
    }
}
