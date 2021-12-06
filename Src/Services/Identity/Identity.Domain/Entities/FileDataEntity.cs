using DomainBase;

namespace Identity.Domain
{
    public class FileDataEntity : Entity
    {
        /// <summary>
        /// 文件名称
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 文件类型
        /// </summary>
        public int? FileType { get; set; }

        /// <summary>
        /// 文件大小
        /// </summary>
        public long? FileSize { get; set; }

        /// <summary>
        /// 文件路径
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// 文件加密
        /// </summary>
        public string FileMd5 { get; set; }
    }
}
