using ElasticSearch.Engine;
using Panosen.Resource.CSharp;
using System;
using System.IO;

namespace ElasticSearch.Service
{
    public class SetupService
    {
        public void Process(Panosen.Generation.Package package, Param param)
        {
            var folder = Environment.CurrentDirectory;

            var codeFirstFolder = Path.Combine(folder, "CodeFirst");

            CSharpResource cSharpResource = new CSharpResource();

            package.Add(Path.Combine(codeFirstFolder, ".gitignore"), cSharpResource.GetResource(CSharpResourceKeys.__gitignore));

            package.Add(Path.Combine(codeFirstFolder, $"{param.ProjectName}.sln"), new SolutionEngine
            {
                ProjectName = param.ProjectName,
                SolutionGuid = param.SolutionGuid,
                ProjectGuid = param.ProjectGuid
            }.TransformText());

            //project
            {
                var projectFolder = Path.Combine(codeFirstFolder, param.ProjectName);

                package.Add(Path.Combine(projectFolder, $"{param.ProjectName}.csproj"), new ProjectEngine
                {
                    RootNamespace = param.AssemblyName,
                    AssemblyName = param.AssemblyName
                }.TransformText());

                //example
                package.Add(Path.Combine(projectFolder, "Product.cs"), new ProductEngine
                {
                    RootNamespace = param.AssemblyName
                }.TransformText());

                //example
                package.Add(Path.Combine(projectFolder, "Book.cs"), new BookEngine
                {
                    RootNamespace = param.AssemblyName
                }.TransformText());
            }

        }
    }
}
