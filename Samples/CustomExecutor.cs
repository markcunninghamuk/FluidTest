using Marktek.Fluent.Testing.Engine.Interfaces;
using System;

namespace Marktek.Fluent.Testing.Engine.Sample
{
    internal class CustomExecutor : IExecutableAction<Guid>
    {
        public void Execute(Guid id)
        {
            Console.WriteLine($"Executing Action with parentid {id}");
        }
    }
}