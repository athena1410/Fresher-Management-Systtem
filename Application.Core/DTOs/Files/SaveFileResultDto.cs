namespace Application.Core.DTOs.Files
{
    public class SaveFileResultDto
    {
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public long ContentLength { get; set; }
        public string FileExtension { get; set; }
        public string SystemFileName { get; set; }
        public string SystemFilePath { get; set; }
    }
}
