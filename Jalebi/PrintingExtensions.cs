
namespace Go
{
    using System;
    using System.Text;
    using static System.Console;
    using static Util;
    public static class PrintingExtensions
    {
        private static readonly string _tripleTab = "\t \t \t";
        private static readonly string tab = "\t";
        private static readonly string newLine = "\n";
        public static void PrintJobDetails(this JobDetails jobDetail, string pipelineName, string stageName, string jobName, int maxWaitTime)
        {
            var waitingTime = (jobDetail.cruise_timestamp_02_assigned - jobDetail.cruise_timestamp_01_scheduled);
            if ((waitingTime.Minutes * 60 + waitingTime.Seconds) > maxWaitTime)
            {
                WriteToConsoleWithColor(tab + pipelineName + tab + stageName + tab + jobName, ConsoleColor.Cyan);
                WriteLine(_tripleTab + " Start Time : " + jobDetail.cruise_timestamp_01_scheduled);
                WriteToConsoleWithColor(_tripleTab + "Wait Time : " + waitingTime, ConsoleColor.Red);
                WriteLine(_tripleTab + " Build Time : " + (jobDetail.cruise_timestamp_06_completed - jobDetail.cruise_timestamp_02_assigned));
                WriteLine();
            }

        }

        public static string PrintJobDetailsWithMarkdown(this JobDetails jobDetail, string pipelineName, string stageName, string jobName, int maxWaitTime)
        {
            var waitingTime = (jobDetail.cruise_timestamp_02_assigned - jobDetail.cruise_timestamp_01_scheduled);
            var output = new StringBuilder();
            if ((waitingTime.Minutes * 60 + waitingTime.Seconds) > maxWaitTime)
            {
                output.Append(tab + pipelineName + tab + stageName + tab + jobName);
                output.Append(_tripleTab + " Start Time : " + jobDetail.cruise_timestamp_01_scheduled);
                output.Append(_tripleTab + " Wait Time : " + waitingTime);
                output.Append(_tripleTab + " Build Time : " + (jobDetail.cruise_timestamp_06_completed - jobDetail.cruise_timestamp_02_assigned));
                output.Append(newLine);

            }
            return output.ToString();
        }
    }
}
