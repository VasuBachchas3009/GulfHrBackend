using GulfHrBackend.DLL.DTO.CreateScheduleDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GulfHrBackend.DLL.DTO
{
    public class CreateScheduleDto
    {
        public string Name {  get; set; }
        public string Type {  get; set; }
        public Guid ModuleId { get; set; }
        public Guid EmailTemplateId {  get; set; }
        public CreateReportScheduleDto ReportSchedule { get; set; }
        public List<RecipientsDto> Recipients { get; set; }

    }
}
