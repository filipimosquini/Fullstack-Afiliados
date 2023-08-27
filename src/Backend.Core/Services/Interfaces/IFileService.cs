using Backend.Core.Services.DataTransferObjects;
using Microsoft.AspNetCore.Http;

namespace Backend.Core.Services.Interfaces;

public interface IFileService
{
    Task<IEnumerable<TransactionDto>> ExtractDataFromFileAsync(IFormFile file);
}