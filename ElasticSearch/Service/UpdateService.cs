using System;
using System.IO;
using System.Reflection;
using ElasticSearch.Engine;
using Microsoft.Extensions.Options;
using Panosen.ElasticSearch.Java;
using Panosen.ElasticSearch.Java.Engine.Engine;
using Panosen.Reflection;
using Panosen.Reflection.Model;

namespace ElasticSearch.Service
{
    public class UpdateService
    {
        private ElasticSearchConfig elasticSearchConfig;

        public UpdateService(IOptions<ElasticSearchConfig> elasticSearchConfig)
        {
            this.elasticSearchConfig = elasticSearchConfig.Value;
        }

        public void Process(Panosen.Generation.Package package, Param param)
        {
            Assembly assembly = Assembly.LoadFrom(param.DLLPath);

            XmlDoc xmlDoc = XmlLoader.LoadXml(param.XMLPath);

            var assemblyFullName = assembly.GetName().Name;

            var assemblyModel = AssemblyLoader.LoadAssembly(assembly, xmlDoc);

            if (assemblyModel == null || assemblyModel.ClassNodeList == null || assemblyModel.ClassNodeList.Count == 0)
            {
                return;
            }

            var folder = Environment.CurrentDirectory;

            var projectFolder = Path.Combine(folder, "Java");

            var srcPath = Path.Combine(projectFolder, "src", "main", "java");

            package.Add(Path.Combine(projectFolder, "pom.xml"), new PomXmlEngine
            {
                GroupId = param.GroupId,
                ArtifactId = param.ArtifactId,
                Version = param.Version,
                ParentGroupId = param.ParentGroupId,
                ParentArtifactId = param.ParentArtifactId,
                ParentVersion = param.ParentVersion,
                Properties = param.Properties,
                BuiltInProperties = elasticSearchConfig.BuiltInProperties
            }.TransformText());

            package.Add(Path.Combine(projectFolder, ".gitignore"), new JavaIgnoreEngine().TransformText());

            foreach (var classNode in assemblyModel.ClassNodeList)
            {
                package.Add(Path.Combine(srcPath, classNode.FullName.Replace(assemblyFullName, param.JavaRootPackage).Replace(".", "\\") + ".java"), new DocEntity
                {
                    RootNamespace = param.AssemblyName,
                    JavaRoot = param.JavaRootPackage,
                    ClassNode = classNode
                }.TransformText());

                package.Add(Path.Combine(srcPath, classNode.FullName.Replace(assemblyFullName, param.JavaRootPackage).Replace(".", "\\") + "Fields.java"), new DocFields
                {
                    RootNamespace = param.AssemblyName,
                    JavaRoot = param.JavaRootPackage,
                    ClassNode = classNode
                }.TransformText());
            }
        }
    }
}
