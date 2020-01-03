using Marktek.Fluent.Testing.Engine.Interfaces;
using System;

namespace Marktek.Fluent.Testing.Engine.Sample.ExportApplications.Cleanup
{
    public class Cleanup : IRecordCleanup<Guid>
    {
        private Guid guid;

        public Cleanup(Guid guid)
        {
            this.guid = guid;
        }

        void IRecordCleanup<Guid>.Cleanup()
        {
            Console.WriteLine($"clean record using {guid}");
        }
    }
}
