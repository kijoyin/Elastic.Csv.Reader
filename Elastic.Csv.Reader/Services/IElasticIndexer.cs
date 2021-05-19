using System.Threading.Tasks;

namespace Elastic.Csv.Reader.Services
{
    public interface IElasticIndexer
    {
        Task<bool> IndexFile(string filePath);
    }
}