namespace Workflow.Api.Models.Entities
{
    public class FileEntity
    {
        public string FileName { get; set; }
        public string MimeType { get; set; }
        public byte[] Content { get; set; }
    }
}