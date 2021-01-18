using FluentAssertions;
using Marktek.Fluent.Testing.Engine.Tests.Models;
using MarkTek.Fluent.Testing.RecordGeneration;
using System;

namespace Marktek.Fluent.Testing.Engine.Tests
{
    internal class EnsureAggregateIdMatches : ISpecificationValidator<DummyModel>
    {
        private Guid id;

        public EnsureAggregateIdMatches(Guid idToCompareAgainst)
        {
            this.id = idToCompareAgainst;
        }

        public void Validate(DummyModel item)
        {
            item.Id.Should().NotBeEmpty();
            item.Id.Should().Be(id);
        }
    }
}