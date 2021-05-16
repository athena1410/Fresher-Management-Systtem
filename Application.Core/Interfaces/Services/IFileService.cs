using Application.Core.DTOs.Files;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

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
