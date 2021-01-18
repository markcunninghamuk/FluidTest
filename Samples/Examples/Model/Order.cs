using System;

namespace MarkTek.Fluent.Testing.ExportApplications.Model
{
    public class Order
    {
        public Order()
        {
            this.Id = Guid.NewGuid();
        }

        public Guid Id { get; }
        public Guid OrderId { get; internal set; }
    }
}
