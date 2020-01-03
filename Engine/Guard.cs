using System;

namespace Marktek.Fluent.Testing.Engine
{
    /// <summary>
    /// Guard Class
    /// </summary>
    public class Guard
    {
        /// <summary>
        /// Guard Against T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        internal static void AgainstNull<T>(T objectToValidate)
        {
            if (objectToValidate.Equals(default(T)))
                throw new ArgumentNullException($"{nameof(objectToValidate)} is null");
        }
    }
}
