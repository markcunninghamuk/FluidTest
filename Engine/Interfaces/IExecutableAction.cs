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
    public interface IExecutableAction<TID>
    {

        void Execute(TID id);
    }

}
