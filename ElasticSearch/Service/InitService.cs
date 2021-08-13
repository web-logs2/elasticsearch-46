using ElasticSearch.Template;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;

namespace ElasticSearch.Service
{
    public class InitService
    {
        private readonly ElasticSearchConfig elasticSearchConfig;

        public InitService(IOptions<ElasticSearchConfig> elasticSearchConfig)
        {
            this.elasticSearchConfig = elasticSearchConfig.Value;
        }

        public void Generate(Panosen.Generation.Package package)
        {
            Param param = new Param();
            param.ProjectName = elasticSearchConfig.CodeFirstProjectName;
            param.AssemblyName = elasticSearchConfig.CodeFirstAssemblyName;
            param.ProjectGuid = Guid.NewGuid().ToString("B").ToUpper();
            param.SolutionGuid = Guid.NewGuid().ToString("B").ToUpper();
            param.JavaRootPackage = elasticSearchConfig.JavaRootPackage;

            param.GroupId = elasticSearchConfig.GroupId;
            param.ArtifactId = elasticSearchConfig.ArtifactId;
            param.Version = elasticSearchConfig.Version;
            param.ParentGroupId = elasticSearchConfig.ParentGroupId;
            param.ParentArtifactId = elasticSearchConfig.ParentArtifactId;
            param.ParentVersion = elasticSearchConfig.ParentVersion;
            param.Properties = elasticSearchConfig.Properties;

            package.Add("elasticsearch.json", JsonConvert.SerializeObject(param, Formatting.Indented, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Include
            }));

            package.Add("update.bat", new UpdateBatEngine().TransformText());
        }
    }
}
