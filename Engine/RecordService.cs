namespace MarkTek.Fluent.Testing.RecordGeneration
{
    public class RecordService<TID> : IRecordService<TID>
    {
        private TID CurrentId { get; set; }
      
        public RecordService<TID> CreateRecord<TEntity>(IRecordCreator<TEntity, TID> app)
        {
            this.CurrentId = app.CreateRecord().Id;
            return this;
        }

        public RecordService<TID> CreateRelatedRecord<TEntity>(IRelatedRecordCreator<TEntity, TID> app)
        {
            app.CreateRecord(CurrentId);
            return this;
        }

        public void Cleanup()
        {
        }

        public void AssertAgainst<TSpec>(TSpec spec) where TSpec : ISpecifcation
        {
            spec.Validate();
        }
    }
}