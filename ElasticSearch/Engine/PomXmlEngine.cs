using Panosen.CodeDom.Pom;
using Panosen.CodeDom.Pom.Engine;
using System.Collections.Generic;

namespace ElasticSearch.Engine
{
    public class PomXmlEngine
    {
        public string GroupId { get; set; }
        public string ArtifactId { get; set; }
        public string Version { get; set; }

        public string ParentGroupId { get; set; }
        public string ParentArtifactId { get; set; }
        public string ParentVersion { get; set; }

        public Dictionary<string, string> Properties { get; set; }

        public Dictionary<string, string> BuiltInProperties { get; set; }


        public string TransformText()
        {
            var project = new Project();
            project.GroupId = this.GroupId;
            project.ArtifactId = this.ArtifactId;
            project.Version = this.Version;

            if (!string.IsNullOrEmpty(this.ParentGroupId) && !string.IsNullOrEmpty(this.ParentArtifactId) && !string.IsNullOrEmpty(this.ParentVersion))
            {
                project.Parent = new Package();
                project.Parent.GroupId = this.ParentGroupId;
                project.Parent.ArtifactId = this.ParentArtifactId;
                project.Parent.Version = this.ParentVersion;
            }

            if (this.Properties != null && this.Properties.Count > 0)
            {
                foreach (var item in this.Properties)
                {
                    project.AddProperty(item.Key, item.Value);
                }
            }

            if (this.BuiltInProperties != null && this.BuiltInProperties.Count > 0)
            {
                foreach (var item in this.BuiltInProperties)
                {
                    if (this.Properties != null && this.Properties.ContainsKey(item.Key))
                    {
                        continue;
                    }
                    project.AddProperty(item.Key, item.Value);
                }
            }

            project.AddDependencyManagement("com.google.code.gson", "gson", "2.8.0");

            project.AddPlugin("org.apache.maven.plugins", "maven-compiler-plugin")
                .AddConfiguration("encoding", "UTF-8")
                .AddConfiguration("source", "1.8")
                .AddConfiguration("target", "1.8");

            project.AddPlugin("org.apache.maven.plugins", "maven-javadoc-plugin", "2.6.1")
                .AddConfiguration("encoding", "UTF-8");

            project.AddDependency("com.google.code.gson", "gson");

            return project.TransformText();
        }
    }
}
