using GulfHrBackend.Core.Utility.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GulfHrBackend.DLL.DTO.CreateScheduleDtos
{
    public class ResponseReportScheduleDto
    {
        public Guid Id { get; set; }
        public Guid ScheduleId { get; set; }
        public Guid ReportId { get; set; }
        public DateTime FromDate { get; set; }
        public string ExportFileType { get; set; }
        public string Frequency { get; set; }

        public string? RepeatOnDays { get; set; }
        public int RepeatOnDate { get; set; }
        public string EndsOn { get; set; }

        public DateTime? EndsOnDate { get; set; }
        public int? EndsOnAfter { get; set; }
        
    }
}
