using Marktek.Fluent.Testing.Engine.Interfaces;
using System;

namespace Marktek.Fluent.Testing.Engine.Sample
{
    internal class CustomExecutor : IExecutableAction<CustomOrder, Guid>, IExecutableAggregateAction<CustomOrder, Guid>
    {
        public void Execute()
        {
            throw new NotImplementedException();
        }

        public void Execute(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}