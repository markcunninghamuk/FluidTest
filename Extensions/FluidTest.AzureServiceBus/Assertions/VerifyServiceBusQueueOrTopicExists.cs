using Azure.Messaging.ServiceBus;
using Azure.Messaging.ServiceBus.Administration;
using Marktek.Fluent.Testing.Engine;
using Marktek.Fluent.Testing.Engine.Common;
using MarkTek.Fluent.Testing.RecordGeneration;
using System;
using System.Collections.Generic;
using System.Text;

namespace FluidTest.AzureServiceBus.Assertions
{
    public class VerifyServiceBusQueueOrTopicExists : BaseValidator<string, bool>
    {
        private ServiceBusAdministrationClient client;

        public VerifyServiceBusQueueOrTopicExists(ServiceBusAdministrationClient client)
        {
            this.client = client;
        }
        public override bool GetRecord(string id)
        {
            return this.client.QueueExistsAsync(id).Result;
        }

        public override List<ISpecificationValidator<bool>> GetValidators()
        {
            return new List<ISpecificationValidator<bool>>
            {
                new ShouldBe<bool>(true)
            };
        }
    }
}
