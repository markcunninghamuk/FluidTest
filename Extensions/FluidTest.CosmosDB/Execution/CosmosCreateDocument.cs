//using MarkTek.Fluent.Testing.RecordGeneration;
//using Microsoft.Azure.Cosmos;
//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace FluidTest.CosmosDB.Execution
//{
//    class CosmosCreateDocument : IRecordCreator<CosmosClient, Guid>
//    {
//        private CosmosClient client;
//        private string databaseName;
//        private string containerName;

//        public CosmosCreateDocument(CosmosClient client, string databaseName, string containerName)
//        {
//            this.client = client;
//            this.databaseName = databaseName;
//            this.containerName = containerName;
//        }
//        public Record<CosmosClient, Guid> CreateRecord()
//        {
//            var container = this.client.GetContainer(databaseName, containerName);

//            return new Record<CosmosClient,Guid>(null, Guid.NewGuid(),cleanUp(Guid.NewGuid()));
//        }

//        private Action<Guid> cleanUp(Guid id)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}