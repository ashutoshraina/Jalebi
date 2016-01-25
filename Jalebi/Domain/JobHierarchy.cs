namespace Go
{
    public struct JobHierarchy
    {
        public readonly string EnvironmentName;
        public readonly string PipelineName;
        public readonly string StageName;

        public JobHierarchy(string environmentName , string pipelineName, string stageName) : this()
        {
            EnvironmentName = environmentName;
            PipelineName = pipelineName;
            StageName = stageName;
        }
    }
}
