using GulfHrBackend.Core.Utility.Enums;
using System.ComponentModel.DataAnnotations;

namespace GulfHrBackend.Models
{
    public class NotificationSchedule
    {
        [Key]
        public Guid NotificationScheduleId { get; set; }
        public Guid ScheduleId { get; set; }
        public SchedulerType Type {  get; set; }
        public bool TillEndDate {  get; set; }
        public IfHoliday IfHoliday { get; set; }


    }
}
