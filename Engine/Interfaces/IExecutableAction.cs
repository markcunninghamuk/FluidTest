using System;
using System.Collections.Generic;
using System.Text;

namespace Marktek.Fluent.Testing.Engine.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TID"></typeparam>
    /// <typeparam name="TType"></typeparam>
    public interface IExecutableAction<TID,TType>
    {


        void Execute();

    }

    public interface IExecutableAggregateAction<TType,TID>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        void Execute(TID id);

     

    }
}
