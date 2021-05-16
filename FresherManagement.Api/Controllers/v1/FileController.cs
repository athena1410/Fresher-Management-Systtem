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
        /// <param name="path">File Path.</param>
        /// <returns></returns>
        [HttpGet("{path}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(Description = "Download File By Path.", OperationId = "DownloadFileByPath")]
        public async Task<ActionResult> DownloadAsync(string path)
        {
            // validation and get the file
            if (!await _fileService.ExistsAsync(path))
            {
                return NotFound("File not existed.");
            }

            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(path, out var contentType))
            {
                contentType = "application/octet-stream";
            }

            var bytes = await _fileService.LoadFileAsync(path);
            return File(bytes, contentType, Path.GetFileName(path));
        }

        /// <summary>
        /// Delete file by file path.
        /// </summary>
        /// <param name="path">File Path.</param>
        /// <returns></returns>
        [HttpDelete("{path}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(Description = "Delete File By Path.", OperationId = "DeleteFileByPath")]
        public async Task<ActionResult> DeleteAsync(string path)
        {
            // validation the file
            if (!await _fileService.ExistsAsync(path))
            {
                return NotFound("File not existed.");
            }

            await _fileService.DeleteFileAsync(path);
            return Ok();
        }
    }
}
