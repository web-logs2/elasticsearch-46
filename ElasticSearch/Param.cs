using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElasticSearch
{
    public class Param
    {
        /// <summary>
        /// 项目名称
        /// </summary>
        [JsonProperty("projectName")]
        public string ProjectName { get; set; }

        /// <summary>
        /// CodeFirst 程序集名称
        /// </summary>
        [JsonProperty("assemblyName")]
        public string AssemblyName { get; set; }

        /// <summary>
        /// CodeFirst 项目 Guid
        /// </summary>
        public string ProjectGuid { get; set; }

        /// <summary>
        /// CodeFirst 解决方案 Guid
        /// </summary>
        public string SolutionGuid { get; set; }

        /// <summary>
        /// DLL 路径
        /// </summary>
        [JsonIgnore]
        public string DLLPath { get { return Path.Combine("CodeFirst", $"{ProjectName}", "bin", "Debug", "net472", $"{AssemblyName}.dll"); } }

        /// <summary>
        /// XML 路径
        /// </summary>
        [JsonIgnore]
        public string XMLPath { get { return Path.Combine("CodeFirst", $"{ProjectName}", "bin", "Debug", "net472", $"{AssemblyName}.xml"); } }

        public string JavaRootPackage { get; set; }

        public string GroupId { get; set; }
        public string ArtifactId { get; set; }
        public string Version { get; set; }

        public string ParentGroupId { get; set; }
        public string ParentArtifactId { get; set; }
        public string ParentVersion { get; set; }

        public Dictionary<string, string> Properties { get; set; }
    }
}
