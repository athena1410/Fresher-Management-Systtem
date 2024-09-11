using Application.Core.DTOs.Files;
using Application.Core.Interfaces.Services;
using Common.Guard;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Shared.Files
{
    public class FileService(ILogger<FileService> logger) : IFileService
    {
        private readonly ILogger<FileService> _logger = Guard.NotNull(logger, nameof(logger));

        public async Task<bool> ExistsAsync(string path)
        {
            Guard.IsNotNullOrEmpty(path);
            return await Task.Run(() => File.Exists(path));
        }

        public async Task<byte[]> LoadFileAsync(string path)
        {
            Guard.IsNotNullOrEmpty(path);
            Guard.FileExists(path);
            return await File.ReadAllBytesAsync(path);
        }

        public async Task<List<SaveFileResultDto>> SaveFileAsync(string folderPath, List<IFormFile> files)
        {
            if (files == null || !files.Any() )
            {
                throw new InvalidOperationException($"Can't save empty file.");
            }

            var result = new List<SaveFileResultDto>();

            folderPath = FileUtils.GenerateFolderIfNotExisted(folderPath);
            foreach (IFormFile file in files)
            {
                if (file.Length == 0)
                {
                    continue;
                }

                var newFileName = FileUtils.GenerateUniqueFileName(file);
                var filePath = Path.Combine(folderPath, newFileName);
                var fileInfo = new FileInfo(filePath);
                await using var stream = new FileStream(filePath, FileMode.Create);
                await file.CopyToAsync(stream);
                result.Add(new SaveFileResultDto
                {
                    FileName = file.FileName,
                    FileExtension = fileInfo.Extension,
                    SystemFileName = fileInfo.Name,
                    SystemFilePath = filePath,
                    ContentType = file.ContentType,
                    ContentLength = file.Length
                });
                _logger.LogInformation($"The uploaded file [{fileInfo.Name}] is saved as [{filePath}].");
            }

            return result;
        }

        public async Task DeleteFileAsync(string path)
        {
            Guard.IsNotNullOrEmpty(path);
            Guard.FileExists(path);
            await Task.Run(() => File.Delete(path));
        }
    }
}
