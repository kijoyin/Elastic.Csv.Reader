using CsvHelper;
using Elastic.Csv.Reader.Options;
using Microsoft.Extensions.Options;
using Nest;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elastic.Csv.Reader.Services
{
    public class ElasticIndexer : IElasticIndexer
    {
        private readonly ElasticCsvIndexerOptions _indexerOptions;
        private ElasticClient elasticClient;
        public ElasticIndexer(IOptions<ElasticCsvIndexerOptions> options)
        {
            _indexerOptions = options?.Value;
            var settings = new ConnectionSettings(new Uri(_indexerOptions.Elastic.Node));
            elasticClient = new ElasticClient(settings);
        }

        public async Task<bool> IndexFile(string filePath)
        {
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var records = csv.GetRecords<dynamic>();
                var indexManyAsyncResponse = await elasticClient.IndexManyAsync(records,"elastic");
            }
            return true;
        }
    }
}
