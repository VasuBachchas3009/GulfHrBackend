using GulfHrBackend.Core.Utility.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GulfHrBackend.Models
{
    public class Schedule
    {
        [Key]
        public Guid ScheduleId { get; set; }
        public string Name { get; set; }
        public SchedulerType Type { get; set; }
        public Guid ModuleId { get; set; }
        public Guid EmailTemplateId { get; set; }
        public bool IsActive {  get; set; }
        public Guid CompanyId { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime CreatedDate { get; set;}
        public bool IsDeleted { get; set; }
        public Guid? DeletedBy {  get; set; }
        public DateTime? DeletedDate { get; set; }
        public Guid LastUpdatedBy { get; set; }
        public DateTime LastUpdatedDate { get; set;}
        public Guid TenantId {  get; set; }

    }
}
