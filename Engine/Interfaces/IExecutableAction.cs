using System;
using System.Collections.Generic;
using System.Text;

namespace Marktek.Fluent.Testing.Engine.Interfaces
{
 /// <summary>
 /// 
 /// </summary>
 /// <typeparam name="TType"></typeparam>
 /// <typeparam name="TID"></typeparam>
    public interface IExecutableAction<TType,TID>
    {

        void Execute(TID id);
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
