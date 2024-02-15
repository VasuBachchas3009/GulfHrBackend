using GulfHrBackend.Core.Utility.Enums;
using GulfHrBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GulfHrBackend.DLL.DTO
{
    public class ReportScheduleDto
    {
        public Guid Id { get; set; }
        public Guid ScheduleId { get; set; }
        public Guid ReportId { get; set; }
        public string FromDate { get; set; }
        public string ExportFileType { get; set; }
        public string Frequency { get; set; }
        public string RepeatOnDays { get; set; }
        public int RepeatOnDate { get; set; }
        public string EndsOn { get; set; }
        public string EndsOnDate { get; set; }
        public int EndsOnAfter { get; set; }
        public List<ReportScheduleRecipients> ReportSceduleRecipients { get; set; }
    }
}

