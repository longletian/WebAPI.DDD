using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Workflow.Api.Models.Entities
{
    [Table("workflow_activity_instance_user")]
    public class WorkflowActivityInstanceUser
    {
        public string WorkflowActivityInstanceId { get; set; }
        public int UserId { get; set; }
    }
}