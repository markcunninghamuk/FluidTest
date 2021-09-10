using MarkTek.Fluent.Testing.RecordGeneration;
using Microsoft.Azure.Cosmos;

namespace FluidTest.CosmosDB.Validators
{
    internal class ContainerMustNotBeNull : ISpecificationValidator<Container>
    {
        public void Validate(Container item)
        {
            throw new System.NotImplementedException();
        }
    }
}