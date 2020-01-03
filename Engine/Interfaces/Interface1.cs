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
        /// 
        /// </summary>
        /// <param name="id"></param>
        void Cleanup();
    }
}
