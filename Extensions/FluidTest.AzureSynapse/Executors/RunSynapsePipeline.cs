using Azure.Analytics.Synapse.Artifacts;
using Azure.Analytics.Synapse.Artifacts.Models;
using MarkTek.Fluent.Testing.RecordGeneration;
using System.Collections.Generic;

namespace FluidTest.AzureSynapse.Executors
{
    public class RunPipeline : IRecordCreator<CreateRunResponse, string>
    {
        private string pipelineName;
        private Dictionary<string, object> pipelineParams;
        private PipelineClient pipelineClient;

        public RunPipeline(string pipelineName, Dictionary<string, object> parameters, PipelineClient pipelineClient)
        {
            this.pipelineName = pipelineName;
            this.pipelineParams = parameters;
            this.pipelineClient = pipelineClient;
        }

        public Record<CreateRunResponse, string> CreateRecord()
        {
            var run = this.pipelineClient.CreatePipelineRun(this.pipelineName, parameters: pipelineParams);
            return new Record<CreateRunResponse, string>(run, run.Value.RunId);
        }
    }
}