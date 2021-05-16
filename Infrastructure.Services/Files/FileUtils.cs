using System;
using Application.Core.Constants;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Shared.Files
{
    public class FileUtils
    {
        /// <summary>
        /// Generate folder if not existed and return folder path.
        /// </summary>
        /// <param name="folderPath">Folder Path.</param>
        /// <returns></returns>
        public static string GenerateFolderIfNotExisted(string folderPath)
        {
            folderPath = Path.Combine(Constants.FolderPath.ASSETS, folderPath);
            // Create folder if not existed
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            return folderPath;
        }

        public static string GenerateUniqueFileName(IFormFile file)
        {
            var fileName = Path.GetFileNameWithoutExtension(file.FileName);
            var extension = Path.GetExtension(file.FileName);

            return $"{fileName}-{Guid.NewGuid()}{extension}";
        }
    }
}
