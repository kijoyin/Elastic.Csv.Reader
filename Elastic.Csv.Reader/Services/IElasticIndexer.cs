using System.IO;
using System.Threading.Tasks;

namespace Elastic.Csv.Reader.Services
{
    public interface IElasticIndexer
    {
        Task<string> IndexFile(FileInfo filePath);
    }
}