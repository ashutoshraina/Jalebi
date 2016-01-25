
namespace Go
{
    using System.Collections.Generic;

    public class Pipeline
    {
        public string label { get; set; }
        public List<Material> materials { get; set; }
        public string name { get; set; }
        public List<Stage> stages { get; set; }
    }
}
