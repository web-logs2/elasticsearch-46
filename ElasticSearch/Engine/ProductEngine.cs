using Panosen.CodeDom.CSharp;
using Panosen.CodeDom.CSharp.Engine;
using Panosen.Language.CSharp;

namespace ElasticSearch.Engine
{
    public class ProductEngine
    {
        public string RootNamespace { get; set; }

        public string TransformText()
        {
            CodeFile codeFile = new CodeFile();

            codeFile.AddSystemUsing("System");
            codeFile.AddSystemUsing("System.Collections.Generic");
            codeFile.AddSystemUsing("System.Linq");
            codeFile.AddSystemUsing("System.Text");
            codeFile.AddSystemUsing("System.Threading.Tasks");

            codeFile.AddNugetUsing("Panosen.ElasticSearch");

            var codeNamespace = codeFile.AddNamespace(this.RootNamespace);

            var codeClass = codeNamespace.AddClass("Product")
                .SetSummary("产品")
                .SetAccessModifiers(AccessModifiers.Public);

            codeClass.AddAttribute("Index");

            codeClass.AddProperty(CSharpTypeConstant.INT, "Id").SetSummary("产品编号");

            codeClass.AddProperty(CSharpTypeConstant.STRING, "Name").SetSummary("产品名称");

            codeClass.AddProperty("List<int>", "StartCityId").SetSummary("出发城市");

            codeClass.AddProperty("List<long>", "DistrictId").SetSummary("关联景区");

            codeClass.AddProperty("List<string>", "SaleMode").SetSummary("销售模式");

            codeClass.AddProperty("Dictionary<string, int>", "StringIntMap").SetSummary("`string`到`long`的映射");

            codeClass.AddProperty("Dictionary<long, string>", "LongStringMap").SetSummary("`long`到`string`的映射");

            codeClass.AddProperty(CSharpTypeConstant.DATETIME, "StartTime").SetSummary("开始时间");

            codeClass.AddProperty(CSharpTypeConstant.DATETIME, "EndTime").SetSummary("结束时间");

            return codeFile.TransformText();
        }
    }
}
