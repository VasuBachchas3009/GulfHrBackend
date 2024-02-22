using System.ComponentModel.DataAnnotations;

namespace GulfHrBackend.Models
{
    public class ReportScheduleRecipients
    {
        public Guid Id {  get; set; }
        public Guid ScheduleId { get; set; }  
        public Guid CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid? DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
        public bool IsDeleted { get; set; } 
        public string UserType { get; set; }
        public Guid LastUpdatedBy { get; set; }
        public DateTime LastUpdatedDate { get; set;}
    }
}
