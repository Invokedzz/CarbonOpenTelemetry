using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;

namespace Infrastructure.Data.Configuration.Reader
{
    public static class CarbonCsvReader
    {
        public static IEnumerable<List<T>> ReadCsvInChunks<T, TMap>(string filePath, int chunkSize = 20_000)
            where T : class where TMap : ClassMap<T>, new()
        {
            using var reader = new StreamReader(filePath, detectEncodingFromByteOrderMarks: true);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

            csv.Context.RegisterClassMap<TMap>();
            var chunk = new List<T>(chunkSize);

            foreach (var record in csv.GetRecords<T>())
            {
                chunk.Add(record);
                if (chunk.Count == chunkSize)
                {
                    yield return chunk;
                    chunk = new List<T>(chunkSize);
                }
            }
            
            if (chunk.Count > 0) 
                yield return chunk;
        }
        
        public static async IAsyncEnumerable<List<T>> ReadCsvInChunksAsync<T, TMap>(string filePath, int chunkSize = 20_000) 
            where T : class where TMap : ClassMap<T>, new()
        {
            using var reader = new StreamReader(filePath, detectEncodingFromByteOrderMarks: true);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            
            csv.Context.RegisterClassMap<TMap>();
            var chunk = new List<T>(chunkSize);
            
            await foreach (var record in csv.GetRecordsAsync<T>())
            {
                chunk.Add(record);
                if (chunk.Count == chunkSize)
                {
                    yield return chunk;
                    chunk = new List<T>(chunkSize);
                }
            }
            
            if (chunk.Count > 0) 
                yield return chunk;
        }
    }
}