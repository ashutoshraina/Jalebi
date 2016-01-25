namespace Go
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using CsvHelper;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using static Util;

    public class Timing : GoApi
    {
        private readonly int jobLimitCount;

        public Timing(Credentials credentials, GoUrls urls, int jobLimitCount) : base(credentials, urls)
        {
            this.jobLimitCount = jobLimitCount;
        }

        public async Task<List<JobDetails>> GetJobDetailsAsync(string pipelineName, string stageName, string jobName)
        {
            try
            {
                    var searchUrl = Urls.SearchUrl;
                    var query = new NameValueCollection {
                                                   {nameof(searchUrl), searchUrl},
                                                   {nameof(pipelineName), pipelineName},
                                                   {nameof(stageName), stageName},
                                                   {nameof(jobName), jobName},
                                                   {@"limitCount", jobLimitCount.ToString()},
                                                   {@"limitPipeline", "latest"}
                                              };

                    var uri = new Uri(searchUrl).AttachParameters(query);
                    var stream = await Dispatch.GetRawStreamAsync(uri);
                    using (var reader = new StreamReader(stream))
                    {
                        using (var csvReader = new CsvReader(reader))
                        {
                            var records = csvReader.GetRecords<JobDetails>();
                            return records.ToList();
                        }
                    }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Enumerable.Empty<JobDetails>().ToList();
            }
        }

        public async Task<List<JobHierarchy>> GetPipelineDetailsAsync()
        {
            var searchUrl = Urls.PipelineGroups;
            var uri = new Uri(searchUrl);
            var rawData = await Dispatch.GetRawDataAsync(uri);
            var envs = JsonConvert.DeserializeObject<JArray>(rawData);
            var environments = envs.ToObject<List<Environment>>();

            var jobHeirarchies = Enumerable.Empty<JobHierarchy>().ToList();
            foreach (var env in environments)
            {
                foreach (var pipeline in env.pipelines)
                {
                    foreach (var stage in pipeline.stages)
                    {
                        jobHeirarchies.Add (new JobHierarchy(env.name, pipeline.name, stage.name));
                    }
                }
            }

            return jobHeirarchies;
        }

        public async Task<List<Job>> GetJobsAsync(string pipelineName, string stageName)
        {
            try
            {
                var searchUrl = Urls.StageHistory(pipelineName, stageName);
                var uri = new Uri(searchUrl);
                var rawData = await Dispatch.GetRawDataAsync(uri);
                var rawStages = JsonConvert.DeserializeObject<JToken>(rawData);
                var stages = rawStages.Value<JArray>("stages");
                var rawJobs = stages.First().Value<JArray>("jobs");
                var jobs = rawJobs.ToObject<List<Job>>();
                return jobs;
            }
            catch (Exception exception)
            {
                WriteToConsoleWithColor("Broken---" + pipelineName + "---" + stageName, ConsoleColor.Red);
                WriteToConsoleWithColor(exception.Message, ConsoleColor.Red);
                return Enumerable.Empty<Job>().ToList();
            }
        }
    }
}
