using Marktek.Fluent.Testing.Engine.Interfaces;

namespace Marktek.Fluent.Testing.Engine.Tests
{
    internal class DummyWaitForWithCustomPolicy : IWaitableAction
    {
        private int attempt = 0;
        public void Execute()
        {
            attempt++;
            if (attempt < 4) throw new System.Exception("Error");
        }
    }
}