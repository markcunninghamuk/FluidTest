using Marktek.Fluent.Testing.Engine.Tests.Models;
using MarkTek.Fluent.Testing.RecordGeneration;
using System;
using System.Collections.Generic;

namespace Marktek.Fluent.Testing.Engine.Tests
{
    internal class DummyAssertionExample : BaseValidator<Guid, DummyModel>
    {
        private Guid id;

        public DummyAssertionExample(Guid passedInExternalId)
        {
            this.id = passedInExternalId;
        }

        public override DummyModel GetRecord(Guid id)
        {
            return new DummyModel
            {
                Id = id
            };
        }

        public override List<ISpecificationValidator<DummyModel>> GetValidators()
        {
            return new List<ISpecificationValidator<DummyModel>>
            {
                  new EnsureAggregateIdMatches(id)
            };
         
        }
    }
}