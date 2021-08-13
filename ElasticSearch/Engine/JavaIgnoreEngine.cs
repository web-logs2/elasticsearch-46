namespace ElasticSearch.Engine
{
    public class JavaIgnoreEngine
    {
        public string TransformText()
        {
            return @"target/
.idea/
.project
*.iml
";
        }
    }
}
