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
        private IDictionary<string, string> parameterFilters;

        public GetTriggeredPipeline(string pipelineName, PipelineRunClient pipelineClient, DateTimeOffset executionTime, IDictionary<string, string> parameterFilters = default)
        {
            this.pipelineName = pipelineName;
            this.pipelineClient = pipelineClient;
            this.executionTime = executionTime;
            this.parameterFilters = parameterFilters;
        }

        public Record<PipelineRun, string> CreateRecord()
        {
            var filter = new RunFilterParameters(executionTime, executionTime.AddHours(1));
            filter.Filters.Add(new RunQueryFilter(RunQueryFilterOperand.PipelineName, RunQueryFilterOperator.In, new List<string>() { pipelineName }));
            var activeRuns = this.pipelineClient.QueryPipelineRunsByWorkspace(filter).Value.Value;

            if (parameterFilters != null && parameterFilters.Any())
            {
                activeRuns = activeRuns
                    .Where(run => parameterFilters.All(parameterFilter =>
                        run.Parameters.ContainsKey(parameterFilter.Key) &&
                        run.Parameters[parameterFilter.Key] == parameterFilter.Value))
                    .ToList();
            }

            PipelineRun activityRun = activeRuns.LastOrDefault();

            if (activityRun == null)
            {
                throw new ExecutionWaitException($"No Run Id detected for { pipelineName } with the given parameters");
            }
            else
            {
                return new Record<PipelineRun, string>(activityRun, activityRun.RunId, "SynapsePipelineRun");
            }
        }
    }
}
