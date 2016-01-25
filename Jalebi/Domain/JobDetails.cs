
namespace Go
{
    using System;
    public class JobDetails
    {
        public string cruise_agent { get; set; }
        public string cruise_job_duration { get; set; }
        public string cruise_job_id { get; set; }
        public string cruise_job_result { get; set; }
        public int cruise_pipeline_counter { get; set; }
        public string cruise_pipeline_label { get; set; }
        public int cruise_stage_counter { get; set; }
        public DateTime cruise_timestamp_01_scheduled { get; set; }
        public DateTime cruise_timestamp_02_assigned { get; set; }
        public DateTime cruise_timestamp_03_preparing { get; set; }
        public DateTime cruise_timestamp_04_building { get; set; }
        public DateTime cruise_timestamp_05_completing { get; set; }
        public DateTime cruise_timestamp_06_completed { get; set; }
    }
}
