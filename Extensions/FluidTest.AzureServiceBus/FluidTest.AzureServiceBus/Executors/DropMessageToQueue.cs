using System;
using System.Collections.Generic;
using System.Text;
using Azure.Messaging.ServiceBus;
using Marktek.Fluent.Testing.Engine;
using MarkTek.Fluent.Testing.RecordGeneration;

namespace FluidTest.AzureServiceBus.Executors
{
    public class SendMessage : IRecordCreator<ServiceBusMessage, String>
    {
        private readonly ServiceBusMessage message;
        private readonly ServiceBusClient client;
        private readonly string queueName;

        public SendMessage(ServiceBusMessage message, ServiceBusClient client, string queueName)
        {
            this.message = message;
            this.client = client;
            this.queueName = queueName;
        }

        public Record<ServiceBusMessage, string> CreateRecord()
        {          
            ServiceBusSender sender = client.CreateSender(queueName);
           
            sender.SendMessageAsync(message);

            return new Record<ServiceBusMessage, string>(message, message.MessageId,"serviceBusMessage");
        }



    }
}
