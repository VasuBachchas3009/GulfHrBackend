using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GulfHrBackend.Core.Utility.Enums;
namespace GulfHrBackend.Models
{
    public class NotificationTimes
    {
        [Key]
        public Guid Id { get; set; }
        public Guid NotificationScheduleId { get; set; }
        public int Hour {  get; set; }
        public Period period {  get; set; }
    }
}
