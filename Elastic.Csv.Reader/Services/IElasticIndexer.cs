namespace Elastic.Csv.Reader.Services
{
    public interface IElasticIndexer
    {
        bool IndexFile(string filePath);
    }
}