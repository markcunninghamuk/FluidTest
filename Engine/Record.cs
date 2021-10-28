using System;

namespace MarkTek.Fluent.Testing.RecordGeneration
{
    /// <summary>
    /// Generic record class allowing for multiple data types
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TId"></typeparam>
    public class Record<T, TId>
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="record"></param>
        /// <param name="id"></param>
        public Record(T record, TId id, string alias)
        {
            this.Id = id;
            this.Row = record;
            this.Alias = alias;
        }

        /// <summary>
        /// Constructor for Record where T class
        /// </summary>
        /// <param name="record"></param>
        /// <param name="id"></param>
        public Record(T record, TId id, string alias, Action<TId> cleanupHandler) : this(record,id,alias)
        {
            this.CleanupDelegateFunction = cleanupHandler;
        }

        /// <summary>
        /// Id of the Entity
        /// </summary>
        public TId Id { get; private set; }

        /// <summary>
        /// Generic T where T is the actual data record / row
        /// </summary>
        public T Row { get; private set; }
        public string Alias { get; private set; }
        public Action<TId> CleanupDelegateFunction { get; private set; }
    }
}