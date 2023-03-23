using Azure.Analytics.Synapse.Artifacts;
using Marktek.Fluent.Testing.Engine.Exceptions;
using Marktek.Fluent.Testing.Engine.Interfaces;
using System;

namespace FluidTest.AzureSynapse.WaitActions
{
    public class WaitForPipelineStatus : IWaitableAction
    {
        private PipelineRunClient client;
        private string runId;
        private string status;

        public WaitForPipelineStatus(PipelineRunClient runClient, string runId, string status)
        {
            this.client = runClient;
            this.runId = runId;
            this.status = status;
        }

        public void Execute()
        {
            var status = this.client.GetPipelineRun(this.runId);

            if (status.Value.Status != this.status)
            {
                if (status.Value.Status == "Failed")
                    throw new Exception($"Pipeline {status.Value.PipelineName } execution finished with unexpected status { status.Value.Status }! Synapse message: { status.Value.Message }");
                else if (status.Value.Status == "Succeeded")
                    throw new Exception($"Pipeline {status.Value.PipelineName } execution finished with unexpected status { status.Value.Status }!");
                else
                    throw new ExecutionWaitException($"Expected the status {this.status} but got {status.Value.Status}. Backing off and retrying");
            }
        }
    }
}
