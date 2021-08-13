using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElasticSearch
{
    public class ElasticSearchConfig
    {
        public string CodeFirstProjectName { get; set; }

        public string CodeFirstAssemblyName { get; set; }

        public string JavaRootPackage { get; set; }

        public string GroupId { get; set; }
        public string ArtifactId { get; set; }
        public string Version { get; set; }

        public string ParentGroupId { get; set; }
        public string ParentArtifactId { get; set; }
        public string ParentVersion { get; set; }

        public Dictionary<string, string> Properties { get; set; }
        public Dictionary<string, string> BuiltInProperties { get; set; }
    }
}
