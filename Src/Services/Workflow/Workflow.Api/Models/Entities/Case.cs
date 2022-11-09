using System.ComponentModel.DataAnnotations.Schema;

namespace Workflow.Api.Models.Entities
{
    [Table("case")]
    public class Case
    {
        [Column("id")]
        public int Id { get; set; }
        /// <summary>
        /// 案件编号
        /// </summary>
        [Column("code")]
        public string Code { get; set; }

        /// <summary>
        /// 上报渠道
        /// </summary>
        [Column("case_channel_id")]
        public int? CaseChannelId { get; set; }

        /// <summary>
        /// 案件来源标识
        /// </summary>
        [Column("case_source_id")]
        public int? CaseSourceId { get; set; }

        /// <summary>
        /// 案件来源名称
        /// </summary>
        [Column("case_source_name")]
        public string CaseSourceName { get; set; }

        /// <summary>
        /// 问题类型标识
        /// </summary>
        [Column("case_type_id")]
        public int? CaseTypeId { get; set; }

        /// <summary>
        /// 问题类型名称
        /// </summary>
        [Column("case_type_name")]
        public string CaseTypeName { get; set; }

        /// <summary>
        /// 问题大类标识
        /// </summary>
        [Column("case_main_type_id")]
        public int? CaseMainTypeId { get; set; }

        /// <summary>
        /// 问题大类名称
        /// </summary>
        [Column("case_main_type_name")]
        public string CaseMainTypeName { get; set; }

        /// <summary>
        /// 问题小类标识
        /// </summary>
        [Column("case_sub_type_id")]
        public int? CaseSubTypeId { get; set; }

        /// <summary>
        /// 问题小类名称
        /// </summary>
        [Column("case_sub_type_name")]
        public string CaseSubTypeName { get; set; }

        /// <summary>
        /// 街道标识
        /// </summary>
        [Column("street_id")]
        public int? StreetId { get; set; }

        /// <summary>
        /// 街道名称
        /// </summary>
        [Column("street_name")]
        public string StreetName { get; set; }

        /// <summary>
        /// 社区标识
        /// </summary>
        [Column("community_id")]
        public int? CommunityId { get; set; }

        /// <summary>
        /// 社区名称
        /// </summary>
        [Column("community_name")]
        public string CommunityName { get; set; }
        
    }
}