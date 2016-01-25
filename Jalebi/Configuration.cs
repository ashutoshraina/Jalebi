
namespace Go
{
    public struct Configuration
    {
        private readonly int jobCount;
        private readonly int maxWaitTimeout;

        public Configuration(int jobCount, int maxWaitTimeout)
        {
            this.jobCount = jobCount;
            this.maxWaitTimeout = maxWaitTimeout;
        }

        public int JobCount => jobCount;

        public int MaxWaitTimeout => maxWaitTimeout;
    }
}
