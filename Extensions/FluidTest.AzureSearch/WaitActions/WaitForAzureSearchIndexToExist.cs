using Azure.Search.Documents.Indexes;
using Marktek.Fluent.Testing.Engine.Exceptions;
using Marktek.Fluent.Testing.Engine.Interfaces;
using System;

namespace FluidTest.AzureSearch.WaitActions
{
    public class WaitForAzureSearchIndexToExist : IWaitableAction
    {
        private SearchIndexClient searchIndexClient;
        private string indexName;

        public WaitForAzureSearchIndexToExist(SearchIndexClient searchIndexClient, string indexName)
        {
            this.searchIndexClient = searchIndexClient;
            this.indexName = indexName;
        }

        public void Execute()
        {
            try
            {
                var index = searchIndexClient.GetIndex(indexName);
            }
            catch (Exception ex)
            {
                throw new ExecutionWaitException($"Index {indexName} is missing, exception { ex.Message } ");
            }
        }
    }
}