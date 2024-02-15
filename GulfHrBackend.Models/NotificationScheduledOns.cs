using GulfHrBackend.Core.Utility.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GulfHrBackend.Models
{
    public class NotificationScheduledOns
    {
        [Key]
        public Guid Id { get; set; }
        public Guid NotificationScheduleId {  get; set; }
        public int Count {  get; set; }
        public NotificationScheduleOn ScheduleOn {  get; set; } 
    }
}
