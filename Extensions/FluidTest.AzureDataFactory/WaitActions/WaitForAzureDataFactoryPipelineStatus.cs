using Marktek.Fluent.Testing.Engine.Exceptions;
using Marktek.Fluent.Testing.Engine.Interfaces;
using System;
using Microsoft.Azure.Management.DataFactory;

namespace FluidTest.AzureDataFactory.WaitActions
{
    public class WaitForAzureDataFactoryPipelineStatus : IWaitableAction
    {
        private DataFactoryManagementClient client;
        private string runId;
        private string status;
        private string resourceGroup;
        private string workspaceName;

        public WaitForAzureDataFactoryPipelineStatus(DataFactoryManagementClient runClient, string runId, string status, string resourceGroup, string workspaceName)
        {
            this.client = runClient;
            this.runId = runId;
            this.status = status;
            this.resourceGroup = resourceGroup;
            this.workspaceName = workspaceName;
        }

        public void Execute()
        {
            var status = this.client.PipelineRuns.Get(this.resourceGroup, this.workspaceName,this.runId);

            if (status.Status != this.status)
            {
                if (status.Status == "Failed" || status.Status == "Succeeded")
                    throw new Exception($"Pipeline {status.PipelineName } execution finished with unexpected status { status.Status } !");
                else
                    throw new ExecutionWaitException($"Expected the status {this.status} but got {status.Status}. Backing off and retrying");
            }
        }
    }
}