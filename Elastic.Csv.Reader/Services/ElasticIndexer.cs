using Elastic.Csv.Reader.Options;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
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

        public bool IndexFile()
        {
            return true;
        }
    }
}
