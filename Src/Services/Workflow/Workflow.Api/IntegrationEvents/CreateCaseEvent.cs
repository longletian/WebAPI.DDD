using DomainBase;

namespace Workflow.Api.IntegrationEvents
{
    public record CreateCaseEvent : Event
    {
        public string CaseName { get; set; }
        
        public string CaseCode { get; set; }
        
        public string ReporterUserId { get; set; }
        
        public string ReporterUserName { get; set; }
    }
}