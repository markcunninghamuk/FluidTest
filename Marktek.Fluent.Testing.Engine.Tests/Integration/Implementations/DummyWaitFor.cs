using Marktek.Fluent.Testing.Engine.Interfaces;

namespace Marktek.Fluent.Testing.Engine.Tests
{
    internal class DummyWaitFor : IWaitableAction
    {
        private int attempt = 0;
        public void Execute()
        {
            attempt++;
            if (attempt == 1) throw new System.Exception("Error");
        }
    }
}