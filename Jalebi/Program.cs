namespace Go
{
    using System;
    using static System.Console;
    using static Util;
    using System.Threading.Tasks;
    using System.Configuration;

    class Program
    {
        static void Main()
        {
            try
            {
                var baseUrl = ConfigurationManager.AppSettings.Get(@"baseUrl");
                if (string.IsNullOrEmpty(baseUrl))
                {
                    throw new ConfigurationException(nameof(baseUrl), "BaseUrl of Go was not specified in App.config");
                }
                var urls = new GoUrls(baseUrl);

                var authCred = ConfigurationManager.AppSettings.Get(@"authCred");
                if (string.IsNullOrEmpty(authCred))
                {
                    throw new ConfigurationException(nameof(authCred), "Go Credentials no specified, should be of the form user:password");
                }

                var jobCount = int.Parse(ConfigurationManager.AppSettings.Get(@"jobCount"));
                var maxWaitTime = int.Parse(ConfigurationManager.AppSettings.Get(@"maxWaitTime"));
                var configuration = new Configuration(jobCount, maxWaitTime);

                var credential = new Credentials(authCred);
                MainAsync(credential, urls, configuration).Wait();
            }
            catch (ConfigurationException argumentNullException)
            {
                WriteToConsoleWithColor(argumentNullException.ToString(), ConsoleColor.Red);
            }
            catch(FormatException)
            {
                WriteToConsoleWithColor("Error Parsing jobCount or maxWaitTime", ConsoleColor.Red);
            }
            catch (Exception exception)
            {
                WriteToConsoleWithColor("Error : " + exception.Message, ConsoleColor.Red);
            }
            ReadKey();
        }

        static async Task MainAsync(Credentials credential, GoUrls urls, Configuration configuration)
        {
            var timing = new Timing(credential,  urls, configuration.JobCount);
            var jobHeirarchies = await timing.GetPipelineDetailsAsync();
            foreach (var jobHeirarchy in jobHeirarchies)
            {
                var jobs = await timing.GetJobsAsync(jobHeirarchy.PipelineName, jobHeirarchy.StageName);
                foreach (var job in jobs)
                {
                    var jobDetails = await timing.GetJobDetailsAsync(jobHeirarchy.PipelineName, jobHeirarchy.StageName, job.name);
                    jobDetails.ForEach(jd => PrintingExtensions.PrintJobDetails(jd, jobHeirarchy.PipelineName, jobHeirarchy.StageName, job.name, configuration.MaxWaitTimeout));
                }
            }
        }
    }
}
