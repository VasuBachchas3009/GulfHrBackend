using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GulfHrBackend.DLL.DTO
{
    public class NotificationScheduledOnDto
    {
        public Guid Id { get; set; }
        public Guid NotificationScheduleId { get; set; }
        public int Count { get; set; }
        public string ScheduleOn { get; set; }
    }
}
