using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GulfHrBackend.DLL.DTO
{
    public class NotificationTimeDto
    {
        public Guid Id { get; set; }
        public Guid NotificationScheduleId { get; set; }
        public int Hour { get; set; }
        public string Period { get; set; }
    }
}
