using FluidTest.CosmosDB.Validators;
using Marktek.Fluent.Testing.Engine;
using MarkTek.Fluent.Testing.RecordGeneration;
using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Text;

namespace FluidTest.CosmosDB.Assertions
{
    public class CosmosContainerShouldExist : BaseValidator<string, Container>
    {
        private CosmosClient client;
        private string containerId;
        private string databaseName;

        public CosmosContainerShouldExist(CosmosClient c, string containerId, string databaseName)
        {
            this.client = c;
            this.containerId = containerId;
            this.databaseName = databaseName;
        }

        public override Container GetRecord(string id)
        {
            return client.GetContainer(this.databaseName, this.containerId);
        }

        public override List<ISpecificationValidator<Container>> GetValidators()
        {
            return new List<ISpecificationValidator<Container>>
            {
                new ContainerMustNotBeNull(),
            };
        }
    }
}
