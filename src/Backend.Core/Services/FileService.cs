using Backend.Core.Converters;
using Backend.Core.Services.DataTransferObjects;
using Backend.Core.Services.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Backend.Core.Services;

public class FileService : IFileService
{
    public async Task<IEnumerable<TransactionDto>> ExtractDataFromFileAsync(IFormFile file)
    {
        using (StreamReader sr = new StreamReader(file.OpenReadStream()))
        {
            string line;
            var lines = new List<string>();

            while ((line = sr.ReadLine()) != null)
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;
                
                lines.Add(line);
            }

            return lines.ConvertToTransactionsDto();
        }
    }
}