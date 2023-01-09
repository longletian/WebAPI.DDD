using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Workflow.Api.Models.Entities
{
    [Table("workflow_activity_instance_user")]
    public class WorkflowActivityInstanceUser
    {
        [Column("activity_instance_id")]
        public string WorkflowActivityInstanceId { get; set; }

        [Column("user_id")]
        public int UserId { get; set; }
    }
}