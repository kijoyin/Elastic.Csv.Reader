using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elastic.Csv.Reader.Options
{
    public class ElasticCsvIndexerOptions
    {
        public const string ElasticCsvIndexer = "ElasticCsvIndexer";
        public string FolderPath { get; set; }
        public ElasticOptions Elastic { get; set; }
    }
    public class ElasticOptions
    {
        public string Node { get; set; }
    }
}
