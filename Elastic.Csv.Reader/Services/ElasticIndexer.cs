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

        public async Task<string> IndexFile(FileInfo file)
        {
            using (var reader = new StreamReader(file.FullName))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var records = csv.GetRecords<dynamic>();
                var indexName = $"{Path.GetFileNameWithoutExtension(file.Name).ToLower()}-{Guid.NewGuid()}";
                var indexManyAsyncResponse = await elasticClient.IndexManyAsync(records, indexName);
                if(indexManyAsyncResponse.IsValid)
                {
                    return indexName;
                }
            }
            return null;
        }
    }
}
