using GulfHrBackend.Core.Utility.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GulfHrBackend.DLL.DTO
{
    public class ScheduleDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public Guid ModuleId { get; set; }
        public Guid EmailTemplateId { get; set; }
        public bool IsActive { get; set; }
        public ReportScheduleDto ReportSchedule { get; set; }
        public NotificationScheduleDto NotificationSchedule { get; set; }
        
    }
}
