using MarkTek.Fluent.Testing.RecordGeneration;
using FluentAssertions;

namespace Marktek.Fluent.Testing.Engine.Common
{
    public class ShouldBe<T> : ISpecificationValidator<T>
    {
        private object itemToCompare;

        public ShouldBe(T itemToCompare)
        {
            this.itemToCompare = itemToCompare;
        }

        public void Validate(T item)
        {
            item.Should().Be(itemToCompare);
        }

    }
}
