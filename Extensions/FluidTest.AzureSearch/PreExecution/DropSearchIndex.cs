using Azure.Search.Documents.Indexes;
using Marktek.Fluent.Testing.Engine.Interfaces;
using System;
using System.Linq;

namespace FluidTest.AzureSearch.PreExecution
{
    public class DropSearchIndex : IPreExecution
    {
        private string indexName;
        private SearchIndexClient searchIndexClient;

        public DropSearchIndex(string indexName, SearchIndexClient searchIndexClient)
        {
            this.indexName = indexName;
            this.searchIndexClient = searchIndexClient;
        }

        public void Execute()
        {
            var allIndexes = searchIndexClient.GetIndexNames();
            if (allIndexes.AsPages().ToArray().First().Values.Contains(indexName))
                searchIndexClient.DeleteIndex(indexName);
            else
                Console.WriteLine($"Index {indexName} already deleted");
        }
    }
}