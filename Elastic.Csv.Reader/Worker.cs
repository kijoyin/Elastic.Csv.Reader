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
                string[] filePaths = Directory.GetFiles(_indexerOptions.FolderPath, "*.csv");
                foreach (var filePath in filePaths)
                {
                    _elasticIndexer.IndexFile(filePath);
                }
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
