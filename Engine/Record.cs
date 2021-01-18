﻿namespace MarkTek.Fluent.Testing.RecordGeneration
{
    /// <summary>
    /// Generic record class allowing for multiple data types
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TId"></typeparam>
    public class Record<T, TId>
    {
        /// <summary>
        /// Constructor for Record where T class
        /// </summary>
        /// <param name="record"></param>
        /// <param name="id"></param>
        public Record(T record, TId id)
        {
            this.Id = id;
            this.Row = record;
        }

        /// <summary>
        /// Id of the Entity
        /// </summary>
        public TId Id { get; private set; }

        /// <summary>
        /// Generic T where T is the actual data record / row
        /// </summary>
        public T Row { get; private set; }
    }
}