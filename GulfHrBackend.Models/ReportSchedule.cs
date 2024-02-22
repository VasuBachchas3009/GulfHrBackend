using GulfHrBackend.Core.Utility.Enums;
using System.ComponentModel.DataAnnotations;

namespace GulfHrBackend.Models
{
    public class ReportSchedule
    {
        [Key]
        public Guid ReportScheduleId { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public Guid? DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
        public Guid LastUpdatedBy { get; set; }
        public DateTime LastUpdatedDate { get; set; }

        public Guid ScheduleId { get; set; }
        public Guid ReportId { get; set; }

        [DataType(DataType.Date)] 
        public DateTime FromDate { get; set; }
        public ExportFileType ExportFileType { get; set; }
        public ReportFrequency ReportFrequency { get; set; }
        public int RepeatOnDate {  get; set; }
        public string? RepeatOnDays { get; set; }
        public SchedulerEndsOn EndsOn { get; set; }
        [DataType(DataType.Date)]
        public DateTime? EndsOnDate { get; set; }
        public int? EndsOnAfter {  get; set; }
        
    }
}
