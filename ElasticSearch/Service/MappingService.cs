using System;
using System.IO;
using System.Reflection;

using Panosen.ElasticSearch;
using Panosen.ElasticSearch.Mapping;
using Panosen.ElasticSearch.Mapping.Engine;

namespace ElasticSearch.Service
{
    public class MappingService
    {
        public void Process(Panosen.Generation.Package package, Param param)
        {
            Assembly assembly = Assembly.LoadFrom(param.DLLPath);

            var types = assembly.GetTypes();

            var folder = Environment.CurrentDirectory;

            var mappingsFolder = Path.Combine(folder, "Mappings");

            foreach (var type in types)
            {
                var indexAttribute = type.GetCustomAttribute<IndexAttribute>(false);
                if (indexAttribute == null)
                {
                    continue;
                }

                package.Add(Path.Combine(mappingsFolder, $"{type.Name.ToLowerCaseBreakLine()}.json"), new Mappings
                {
                    Type = type
                }.TransformText());
            }
        }
    }
}
