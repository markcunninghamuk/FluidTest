using Marktek.Fluent.Testing.Engine.Interfaces;
using System;

namespace Marktek.Fluent.Testing.Engine.Tests
{
    internal class ExecuteDummyAction : IExecutableAction<Guid>
    {
        public void Execute(Guid id)
        {
           
        }
    }
}