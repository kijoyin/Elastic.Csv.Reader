using Elastic.Csv.Reader.Options;
using Elastic.Csv.Reader.Services;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Elastic.Csv.Reader
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IElasticIndexer _elasticIndexer;
        private readonly ElasticCsvIndexerOptions _indexerOptions;

        public Worker(ILogger<Worker> logger, 
            IElasticIndexer elasticIndexer,
            IOptions<ElasticCsvIndexerOptions> options)
        {
            _logger = logger;
            _elasticIndexer = elasticIndexer;
            _indexerOptions = options?.Value;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                DirectoryInfo di = new DirectoryInfo(_indexerOptions.FolderPath);
                FileInfo[] files = di.GetFiles("*.csv");
                foreach (var file in files)
                {
                    var indexName = await _elasticIndexer.IndexFile(file);
                    if (!string.IsNullOrEmpty(indexName))
                    {
                        file.MoveTo(@$"{file.DirectoryName}\processed\{indexName}{file.Extension}");
                    }
                }
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
