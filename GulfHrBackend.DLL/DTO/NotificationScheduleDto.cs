using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GulfHrBackend.DLL.DTO
{
    public class NotificationScheduleDto
    {
        public Guid Id { get; set; }
        public Guid ScheduleId { get; set; }
        public string Type { get; set; }
        public bool TillEndDate { get; set; }
        public string IfHoliday { get; set; }
        public List<NotificationScheduledOnDto> NotificationScheduledOns { get; set; }
        public List<NotificationTimeDto> NotificationTimes { get; set; }
    }
}
