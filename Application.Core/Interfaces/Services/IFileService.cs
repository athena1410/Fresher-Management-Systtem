using Application.Core.DTOs.Files;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Application.Core.Interfaces.Services
{
    public interface IFileService
    {
        Task<bool> ExistsAsync(string path);
        Task<byte[]> LoadFileAsync(string path);
        Task<List<SaveFileResultDto>> SaveFileAsync(string folderPath, List<IFormFile> files);
        Task DeleteFileAsync(string path);
    }
}
