using System;

namespace Marktek.Fluent.Testing.Engine.Exceptions
{
    public class ExecutionWaitException : Exception
    {
        public ExecutionWaitException(string message) : base(message)
        {
        }
    }
}