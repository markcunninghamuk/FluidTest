using Marktek.Fluent.Testing.Engine.Exceptions;
using MarkTek.Fluent.Testing.RecordGeneration;
using Microsoft.Azure.Management.DataFactory;
using Microsoft.Azure.Management.DataFactory.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FluidTest.AzureDataFactory.Executors
{
    public class GetTriggeredPipeline : IRecordCreator<PipelineRun, string>
    {
        private string pipelineName;
        private DataFactoryManagementClient pipelineClient;
        private DateTime executionTime;
        private string resourceGroup;
        private string workspaceName;

        public GetTriggeredPipeline(string pipelineName, DataFactoryManagementClient pipelineClient, DateTime executionTime, string resourceGroup, string workspaceName)
        {
            this.pipelineName = pipelineName;
            this.pipelineClient = pipelineClient;
            this.executionTime = executionTime;
            this.resourceGroup = resourceGroup;
            this.workspaceName = workspaceName;
        }

        public Record<PipelineRun, string> CreateRecord()
        {
            var filter = new RunFilterParameters(executionTime, executionTime.AddHours(1));
            filter.Filters.Add(new RunQueryFilter(RunQueryFilterOperand.PipelineName, RunQueryFilterOperator.In, new List<string>() { pipelineName }));
            var activeRuns = this.pipelineClient.PipelineRuns.QueryByFactory(this.resourceGroup, this.workspaceName, filter);
            PipelineRun activityRun = activeRuns.Value.LastOrDefault();

            if (activityRun == null)
            {
                throw new ExecutionWaitException($"No Run Id detected for { pipelineName }");
            }
            else
            {
                return new Record<PipelineRun, string>(activityRun, activityRun.RunId, "DataFactoryPipelineRun");
            }
        }
    }
}
