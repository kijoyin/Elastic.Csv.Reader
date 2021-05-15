using Elastic.Csv.Reader.Options;
using Elastic.Csv.Reader.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Elastic.Csv.Reader
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.Configure<ElasticCsvIndexerOptions>(hostContext.Configuration.GetSection(
                                        ElasticCsvIndexerOptions.ElasticCsvIndexer));
                    services.AddSingleton<IElasticIndexer, ElasticIndexer>();
                    services.AddHostedService<Worker>();
                });
    }
}
