﻿using FluentAssertions;
using MarkTek.Fluent.Testing.RecordGeneration;
using Microsoft.Azure.Cosmos;

namespace FluidTest.CosmosDB.Validators
{
    public class ContainerMustNotBeNull : ISpecificationValidator<Container>
    {
        public void Validate(Container item)
        {
            item.Should().NotBeNull();
        }
    }
}