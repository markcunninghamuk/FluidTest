using Marktek.Fluent.Testing.Engine.Interfaces;
using System;

namespace Marktek.Fluent.Testing.Engine.Sample
{
    internal class PreOperation : IPreExecution
    {
        public void Execute()
        {
            Console.WriteLine("Pre Execute Action");
        }
    }
}