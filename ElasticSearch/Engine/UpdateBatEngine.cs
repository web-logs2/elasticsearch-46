using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElasticSearch.Template
{
    public class UpdateBatEngine
    {
        public string TransformText()
        {
            return @"pushd CodeFirst
dotnet restore
dotnet build
popd

rmdir /S /Q Java
rmdir /S /Q Mappings

elasticsearch update

pause
";
        }
    }
}
