
using Panosen.CodeDom.CSharpProject;
using Panosen.CodeDom.CSharpProject.Engine;

namespace ElasticSearch.Engine
{
    public class ProjectEngine
    {
        public string RootNamespace { get; set; }

        public string AssemblyName { get; set; }

        public string TransformText()
        {
            var project = new Project();

            project.Sdk = "Microsoft.NET.Sdk";

            project.AddTargetFramework("net472");
            project.AssemblyName = this.AssemblyName;
            project.RootNamespace = this.RootNamespace;
            project.WithDocumentationFile = true;

            project.AddPackageReference("Panosen.ElasticSearch", "0.2.3");

            return project.TransformText();
        }
    }
}
