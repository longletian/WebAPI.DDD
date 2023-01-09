using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Workflow.Api.Models.Entities
{
    /// <summary>
    /// 事件关联信息表
    /// </summary>
    [Table("workflow_case_instance")]
    public class WorkflowCaseInstance
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// 事件标识
        /// </summary>
        [Column("case_id")]
        public int CaseId { get; set; }

        /// <summary>
        /// 工作流定义标识
        /// </summary>
        [Column("wf_definition_id")]
        public string WorkflowDefinitionId { get; set; }

        /// <summary>
        /// 工作流实例标识
        /// </summary>
        [Column("wf_instance_id")]
        public string WorkflowInstanceId { get; set; }
    }
}