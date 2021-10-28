using Azure.Analytics.Synapse.Artifacts;
using Azure.Analytics.Synapse.Artifacts.Models;
using Marktek.Fluent.Testing.Engine.Exceptions;
using MarkTek.Fluent.Testing.RecordGeneration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FluidTest.AzureSynapse.Executors
{
    public class GetTriggeredPipeline : IRecordCreator<PipelineRun, string>
    {
        private string pipelineName;
        private PipelineRunClient pipelineClient;
        private DateTimeOffset executionTime;

        public GetTriggeredPipeline(string pipelineName, PipelineRunClient pipelineClient, DateTimeOffset executionTime)
        {
            this.pipelineName = pipelineName;
            this.pipelineClient = pipelineClient;
            this.executionTime = executionTime;
        }

        public Record<PipelineRun, string> CreateRecord()
        {
            var filter = new RunFilterParameters(executionTime, executionTime.AddHours(1));
            filter.Filters.Add(new RunQueryFilter(RunQueryFilterOperand.PipelineName, RunQueryFilterOperator.In, new List<string>() { pipelineName }));
            var activeRuns = this.pipelineClient.QueryPipelineRunsByWorkspace(filter);
            PipelineRun activityRun = activeRuns.Value.Value.LastOrDefault();

            if (activityRun == null)
            {
                throw new ExecutionWaitException($"No Run Id detected for { pipelineName }");
            }
            else
            {
                return new Record<PipelineRun, string>(activityRun, activityRun.RunId, "SynapsePipelineRun");
            }
        }
    }
}
