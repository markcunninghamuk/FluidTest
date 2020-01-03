using System.Collections.Generic;

namespace MarkTek.Fluent.Testing.RecordGeneration
{
    /// <summary>
    /// An interface that should be implemented to ensure the tests meet the expected criteria
    /// </summary>
    public interface ISpecifcation<TID,TType>
    {
        /// <summary>
        /// Validates against a set of created records, scenarios
        /// </summary>
        void Validate(TID aggregateId);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        List<ISpecificationValidator<TType>> GetValidators();

    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ISpecificationValidator<T>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        void Validate(T item);
    }

}
