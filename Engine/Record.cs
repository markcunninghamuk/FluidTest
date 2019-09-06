namespace MarkTek.Fluent.Testing.RecordGeneration
{
    public class Record<T, TId>
    {
        public Record()
        {
        }

        public Record(T record, TId id)
        {
            this.Id = id;
            this.Row = record;
        }
        public TId Id { get; set; }
        public T Row { get; set; }
    }
}