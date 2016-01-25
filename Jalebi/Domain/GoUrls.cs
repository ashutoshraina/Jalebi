namespace Go
{
    public class GoUrls
    {
        private readonly string api = "/api";
        private readonly string baseUrl = "";

        public GoUrls(string baseUrl)
        {
            this.baseUrl = baseUrl;
        }

        public string PipelineGroups => baseUrl + api + "/config/pipeline_groups";
        public string SearchUrl => baseUrl + "/properties/search";
        public string StageHistory(string pipelineName, string stageName) =>
                                    baseUrl + api + "/stages" + "/" + pipelineName + "/" + stageName + "/history";
    }
}
