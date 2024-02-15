using System.ComponentModel.DataAnnotations;

namespace GulfHrBackend.Models
{
    public class ReportScheduleRecipients
    {
        [Key]
        public Guid Id {  get; set; }
        public Guid ReportScheduleId { get; set; }  
        public Guid UserId { get; set; }
    }
}
