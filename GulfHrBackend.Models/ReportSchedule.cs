using GulfHrBackend.Core.Utility.Enums;
using System.ComponentModel.DataAnnotations;

namespace GulfHrBackend.Models
{
    public class ReportSchedule
    {
        [Key]
        public Guid ReportScheduleId { get; set; }
        public Guid ScheduleId { get; set; }
        public Guid ReportId { get; set; }

        [DataType(DataType.Date)] 
        public DateTime FromDate { get; set; }
        public ExportFileType ExportFileType { get; set; }
        public ReportFrequency ReportFrequency { get; set; }
        public SchedulerEndsOn EndsOn { get; set; }
        [DataType(DataType.Date)]
        public DateTime EndsOnDate { get; set; }
        public int EndsOnAfter {  get; set; }
        public List<ReportScheduleRecipients> ReportScheduleRecipients { get; set; }

    }
}
