using System;
using System.Collections.Generic;
using System.Text;

namespace Marktek.Fluent.Testing.Engine.Interfaces
{
    public interface IPreExecution : IExecute
    {
    }

    public interface IExecute
    {
        void Execute();
    }
}
