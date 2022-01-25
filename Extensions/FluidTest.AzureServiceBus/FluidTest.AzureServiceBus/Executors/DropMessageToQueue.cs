using System;
using System.Collections.Generic;
using System.Text;
using Azure.Messaging.ServiceBus;
using Marktek.Fluent.Testing.Engine;
using MarkTek.Fluent.Testing.RecordGeneration;

namespace FluidTest.AzureServiceBus.Executors
{
    public class DropMessageToQueue : IRecordCreator<ServiceBusMessage, String>
    {
        public Record<ServiceBusMessage, string> CreateRecord()
        {
            ServiceBusClient client = new ServiceBusClient("Endpoint=sb://markcapddd.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=Hq7SnIQPEMWz3hd76/UxpsKYXMdP7cXmktoJJuIQDhQ=");

            string queueName = "flowtest";

            // create the sender
            ServiceBusSender sender = client.CreateSender(queueName);

            // create a message that we can send. UTF-8 encoding is used when providing a string.
            ServiceBusMessage message = new ServiceBusMessage("Hello world!");
            message.MessageId = Guid.NewGuid().ToString();
            // send the message
            sender.SendMessageAsync(message);

            return new Record<ServiceBusMessage, string>(message, message.MessageId,"serviceBusMessage");
        }



    }
}
