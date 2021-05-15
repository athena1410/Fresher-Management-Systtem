using Application.Core.DTOs.Files;
using Application.Core.Interfaces.Services;
using Common.Guard;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Swashbuckle.AspNetCore.Annotations;
using System.IO;
using Microsoft.AspNetCore.StaticFiles;

namespace FresherManagement.Api.Controllers.v1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    public class FileController : BaseController
    {
        private readonly IFileService _fileService;
        public FileController(IFileService fileService)
        {
            _fileService = Guard.Null(fileService, nameof(fileService));
        }

        /// <summary>
        /// Upload Multiple Files
        /// </summary>
        /// <param name="folderPath">Folder Path.</param>
        /// <param name="files">File Upload</param>
        /// <returns></returns>
        [HttpPost("{folderPath}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation(Description = "Upload Files.", OperationId = "UploadFile")]
        public async Task<ActionResult<List<SaveFileResultDto>>> UploadAsync(string folderPath, [Required] List<IFormFile> files)
        {
            if (files == null || files.Count == 0)
            {
                return BadRequest("No file is uploaded.");
            }

            var result = await _fileService.SaveFileAsync(folderPath, files);
            return Ok(result);
        }

        /// <summary>
        /// Download file by file path.
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        [HttpGet("{filePath}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(Description = "Download File By Path.", OperationId = "DownloadFileByPath")]
        public async Task<ActionResult> DownloadAsync(string filePath)
        {
            // validation and get the file
            if (!System.IO.File.Exists(filePath))
            {
                return NotFound("File not existed.");
            }

            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(filePath, out var contentType))
            {
                contentType = "application/octet-stream";
            }

            var bytes = await System.IO.File.ReadAllBytesAsync(filePath);
            return File(bytes, contentType, Path.GetFileName(filePath));
        }

        /// <summary>
        /// Delete file by file path.
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        [HttpDelete("{filePath}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(Description = "Delete File By Path.", OperationId = "DeleteFileByPath")]
        public async Task<ActionResult> DeleteAsync(string filePath)
        {
            // validation the file
            if (!System.IO.File.Exists(filePath))
            {
                return NotFound("File not existed.");
            }

            await Task.Run(() => System.IO.File.Delete(filePath));
            return Ok();
        }
    }
}
