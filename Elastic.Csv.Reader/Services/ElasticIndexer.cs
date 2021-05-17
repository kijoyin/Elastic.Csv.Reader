using CsvHelper;
using Elastic.Csv.Reader.Options;
using Microsoft.Extensions.Options;
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

        public ElasticIndexer(IOptions<ElasticCsvIndexerOptions> options)
        {
            _indexerOptions = options?.Value;
        }

        public bool IndexFile(string filePath)
        {
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var records = csv.GetRecords<dynamic>();
            }
            return true;
        }
    }
}
