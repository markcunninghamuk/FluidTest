using Azure.Analytics.Synapse.Artifacts;
using Azure.Analytics.Synapse.Artifacts.Models;
using Marktek.Fluent.Testing.Engine.Exceptions;
using MarkTek.Fluent.Testing.RecordGeneration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FluidTest.AzureSynapse.Executors
{
    public class GetTriggeredPipelineByTrigger : IRecordCreator<PipelineRun, string>
    {
        private string triggerName;
        private PipelineRunClient pipelineClient;
        private DateTimeOffset executionTime;

        public GetTriggeredPipelineByTrigger(string triggerName, PipelineRunClient pipelineClient, DateTimeOffset executionTime)
        {
            this.pipelineClient = pipelineClient;
            this.executionTime = executionTime;
            this.triggerName = triggerName;
        }

        public Record<PipelineRun, string> CreateRecord()
        {
            var filter = new RunFilterParameters(executionTime, executionTime.AddHours(1));
            filter.Filters.Add(new RunQueryFilter(RunQueryFilterOperand.TriggerName, RunQueryFilterOperator.EqualsValue, new List<string>() { triggerName }));
            var activeRuns = this.pipelineClient.QueryPipelineRunsByWorkspace(filter);
            PipelineRun activityRun = activeRuns.Value.Value.LastOrDefault();

            if (activityRun == null)
            {
                throw new ExecutionWaitException($"No Run Id detected for {triggerName}");
            }
            else
            {
                return new Record<PipelineRun, string>(activityRun, activityRun.RunId, "SynapseTriggerRun");
            }
        }
    }
}
