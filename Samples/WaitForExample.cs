using Marktek.Fluent.Testing.Engine.Interfaces;

namespace Marktek.Fluent.Testing.Engine.Sample
{
    internal class WaitForExample : IWaitableAction
    {
        //Polling, Waiting for Event Etc, Can be Any type of wait
        //Silly Example but the wait can contain a Polly Class for Example
        public void Execute()
        {
            for (int i = 0; i < 10; i++)
            {
                System.Threading.Thread.Sleep(1);
            }
        }
    }
}