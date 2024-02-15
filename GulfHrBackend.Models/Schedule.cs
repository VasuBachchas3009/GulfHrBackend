using GulfHrBackend.Core.Utility.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GulfHrBackend.Models
{
    public class Schedule
    {
        [Key]
        public Guid ScheduleId { get; set; }
        public string Name { get; set; }
        public SchedulerType Type { get; set; }
        public Guid ModuleId { get; set; }
        public Guid EmailTemplateId { get; set; }
        public bool IsActive {  get; set; }
        public Guid ReportScheduleId { get; set; }
        [ForeignKey("ReportScheduleId")]
        public virtual ReportSchedule ReportSchedule { get; set; }
        public Guid NotificationScheduleId {  get; set; }
        [ForeignKey("NotificationScheduleId")]
        public NotificationSchedule NotificationSchedule { get; set; }


    }
}
