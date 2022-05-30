using MarkTek.Fluent.Testing.RecordGeneration;
using Microsoft.Azure.Management.DataFactory;
using Microsoft.Azure.Management.DataFactory.Models;
using System.Collections.Generic;

namespace FluidTest.AzureDataFactory.Executors
{
    public class RunPipeline : IRecordCreator<CreateRunResponse, string>
    {
        private string pipelineName;
        private Dictionary<string, object> pipelineParams;
        private DataFactoryManagementClient pipelineClient;
        private string resourceGroup;
        private string workspaceName;

        public RunPipeline(string pipelineName, Dictionary<string, object> parameters, DataFactoryManagementClient pipelineClient, string resourceGroup, string workspaceName)
        {
            this.pipelineName = pipelineName;
            this.pipelineParams = parameters;
            this.pipelineClient = pipelineClient;
            this.resourceGroup = resourceGroup;
            this.workspaceName = workspaceName;
        }

        public Record<CreateRunResponse, string> CreateRecord()
        {
            var run = this.pipelineClient.Pipelines.CreateRun(resourceGroup,workspaceName,this.pipelineName, parameters: pipelineParams);
            return new Record<CreateRunResponse, string>(run, run.RunId, "DataFactoryCreatedPipelineRun");
        }
    }
}