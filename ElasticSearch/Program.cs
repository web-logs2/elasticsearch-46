using ElasticSearch.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;

namespace ElasticSearch
{
    class Program
    {
        private static readonly Encoding DefaultEncoding = new UTF8Encoding(false);

        static void Main(string[] args)
        {
            if (args == null || args.Length == 0)
            {
                ShowMenu();
                return;
            }

            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();

            IServiceCollection services = new ServiceCollection();

            services.Configure<ElasticSearchConfig>(configuration.GetSection("ElasticSearchConfig"));

            services.AddSingleton<InitService>();
            services.AddSingleton<SetupService>();
            services.AddSingleton<UpdateService>();
            services.AddSingleton<MappingService>();

            var serviceProvider = services.BuildServiceProvider();

            Process(serviceProvider, args[0]);
        }

        private static void Process(ServiceProvider serviceProvider, string command)
        {
            Panosen.Generation.Package package = new Panosen.Generation.Package();

            if ("init".Equals(command, StringComparison.OrdinalIgnoreCase))
            {
                serviceProvider.GetRequiredService<InitService>().Generate(package);
                Flush(package);
                return;
            }

            if (!File.Exists("elasticsearch.json"))
            {
                Console.WriteLine("elasticsearch.json is required.");
                return;
            }

            var content = File.ReadAllText("elasticsearch.json");
            Param param = JsonConvert.DeserializeObject<Param>(content);

            if ("setup".Equals(command, StringComparison.OrdinalIgnoreCase))
            {
                serviceProvider.GetRequiredService<SetupService>().Process(package, param);
                Flush(package);
                return;
            }

            if ("update".Equals(command, StringComparison.OrdinalIgnoreCase))
            {
                //生成 java 代码
                serviceProvider.GetRequiredService<UpdateService>().Process(package, param);

                //生成 mapping 文件
                serviceProvider.GetRequiredService<MappingService>().Process(package, param);

                Flush(package);

                return;
            }

            ShowMenu();
        }

        private static void Flush(Panosen.Generation.Package package)
        {
            foreach (var item in package.Files)
            {
                var path = Path.Combine(Environment.CurrentDirectory, item.FilePath);

                var fileDirectory = Path.GetDirectoryName(path);
                if (!Directory.Exists(fileDirectory))
                {
                    Directory.CreateDirectory(fileDirectory);
                }

                if (item is Panosen.Generation.PlainFile)
                {
                    File.WriteAllText(path, ((Panosen.Generation.PlainFile)item).Content, DefaultEncoding);
                }

                if (item is Panosen.Generation.BytesFile)
                {
                    File.WriteAllBytes(path, ((Panosen.Generation.BytesFile)item).Bytes);
                }
            }
        }

        static void ShowMenu()
        {
            var menu = @"1. elasticsearch init 创建项目，生成soa.json模版文件
2. elasticsearch setup 初始化项目
3. elasticsearch update 更新生成的文件
";

            Console.WriteLine(menu);
        }
    }
}
