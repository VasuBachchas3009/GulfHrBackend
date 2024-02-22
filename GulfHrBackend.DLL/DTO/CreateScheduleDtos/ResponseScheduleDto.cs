using GulfHrBackend.Core.Utility.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GulfHrBackend.DLL.DTO.CreateScheduleDtos
{
    public class ResponseScheduleDto
    {
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid LastUpdatedBy { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public Guid? DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
        public Guid TenantId { get; set; }
        public Guid CompanyId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public Guid ModuleId { get; set; }
        public Guid EmailTemplateId { get; set; }
        public bool IsActive { get; set; }
        public ResponseReportScheduleDto ReportSchedule { get; set; }
        public List<ResponseRecipientDto> Recipients { get; set; }
        public NotificationScheduleDto? NotificationSchedule { get; set; }
    }
}
