using Application.Core.DTOs.Files;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Core.Interfaces.Services
{
    public interface IFileService
    {
        Task<List<SaveFileResultDto>> SaveFileAsync(string folderPath, List<IFormFile> files);
    }
}
