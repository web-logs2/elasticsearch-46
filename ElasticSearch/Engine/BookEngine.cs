using Panosen.CodeDom.CSharp;
using Panosen.CodeDom.CSharp.Engine;
using Panosen.Language.CSharp;

namespace ElasticSearch.Engine
{
    public class BookEngine
    {
        public string RootNamespace { get; set; }

        public string TransformText()
        {
            CodeFile codeFile = new CodeFile();

            codeFile.AddSystemUsing("System");
            codeFile.AddSystemUsing("System.Collections.Generic");

            codeFile.AddNugetUsing("Panosen.ElasticSearch");

            var codeNamespace = codeFile.AddNamespace(this.RootNamespace);

            var codeClass = codeNamespace.AddClass("Book")
                .SetSummary("图书")
                .SetAccessModifiers(AccessModifiers.Public);

            codeClass.AddAttribute("Index")
                .AddPlainParam("Dynamic", "Dynamic.False")
                .AddPlainParam("NumberOfShards", "1")
                .AddPlainParam("NumberOfReplicas", "4")
                .AddPlainParam("MappingTotalFieldsLimit", "50000");
            codeClass.AddAttribute("NGramTokenizer")
                .AddStringParam("trigram_tokenizer")
                .AddPlainParam("1")
                .AddPlainParam("3")
                .AddPlainParam("NGramTokenChar.Letter | NGramTokenChar.Digit");
            codeClass.AddAttribute("EdgeNGramTokenizer")
                .AddStringParam("edge_ten_tokenizer")
                .AddPlainParam("1")
                .AddPlainParam("10")
                .AddPlainParam("NGramTokenChar.Letter | NGramTokenChar.Digit");
            codeClass.AddAttribute("EdgeNGramTokenizer")
                .AddStringParam("edge_twenty_tokenizer")
                .AddPlainParam("1")
                .AddPlainParam("20")
                .AddPlainParam("NGramTokenChar.Letter | NGramTokenChar.Digit");
            codeClass.AddAttribute("PatternTokenizer")
                .AddStringParam("comma_tokenizer")
                .AddStringParam(",");
            codeClass.AddAttribute("CustomAnalyzer")
                .AddStringParam("trigram_analyzer")
                .AddStringParam("trigram_tokenizer")
                .AddPlainParam("BuiltInTokenFilters", "BuiltInTokenFilters.LOWERCASE");
            codeClass.AddAttribute("CustomAnalyzer")
                .AddStringParam("edge_ten_analyzer")
                .AddStringParam("edge_ten_tokenizer")
                .AddPlainParam("BuiltInTokenFilters", "BuiltInTokenFilters.LOWERCASE");
            codeClass.AddAttribute("CustomAnalyzer")
                .AddStringParam("edge_twenty_analyzer")
                .AddStringParam("edge_twenty_tokenizer")
                .AddPlainParam("BuiltInTokenFilters", "BuiltInTokenFilters.LOWERCASE");
            codeClass.AddAttribute("CustomAnalyzer")
                .AddStringParam("comma_analyzer")
                .AddStringParam("comma_tokenizer");


            codeClass.AddProperty(CSharpTypeConstant.INT, "AssumeInt").SetSummary("推断 int");

            codeClass.AddProperty(CSharpTypeConstant.LONG, "AssumeLong").SetSummary("推断 long");

            codeClass.AddProperty(CSharpTypeConstant.STRING, "AssumeText").SetSummary("推断 text");

            codeClass.AddProperty(CSharpTypeConstant.INT, "UseInteger").SetSummary("使用 int")
                .AddAttribute("IntegerField").AddPlainParam("123");

            codeClass.AddProperty(CSharpTypeConstant.LONG, "UseLong").SetSummary("使用 long")
                .AddAttribute("LongField");

            codeClass.AddProperty(CSharpTypeConstant.STRING, "UseKeyword").SetSummary("使用 keyword")
                .AddAttribute("KeywordField").AddPlainParam("IgnoreAbove", "128");

            codeClass.AddProperty(CSharpTypeConstant.STRING, "UseText").SetSummary("使用 text")
                .AddAttribute("TextField");

            codeClass.AddProperty(CSharpTypeConstant.STRING, "NotIndexMe").SetSummary("只存储，不索引")
                .AddAttribute("TextField").AddPlainParam("Index", "Index.False");

            codeClass.AddProperty(CSharpTypeConstant.STRING, "WithNullValue").SetSummary("null_value")
                .AddAttribute("TextField").AddStringParam("NullValue", "NULL");

            var property = codeClass.AddProperty(CSharpTypeConstant.STRING, "UseAnalyzer").SetSummary("使用分词器");
            property.AddAttribute("TextField");
            property.AddAttribute("WithTextFields").AddPlainParam("IKAnalyzer.IK_SMART");
            property.AddAttribute("WithTextFields").AddPlainParam("IKAnalyzer.IK_MAX_WORD");
            property.AddAttribute("WithTextFields").AddPlainParam("BuiltInAnalyzer.Simple");
            property.AddAttribute("WithTextFields").AddPlainParam("BuiltInAnalyzer.Whitespace");
            property.AddAttribute("WithTextFields").AddStringParam("ngram_1_1");

            codeClass.AddProperty(CSharpTypeConstant.STRING, "WithDefaultAnalyzer").SetSummary("带默认分析器")
                .AddAttribute("TextField").AddPlainParam("IKAnalyzer.IK_SMART");

            return codeFile.TransformText();
        }
    }
}
