using GulfHrBackend.DLL.DTO;
using GulfHrBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GulfHrBackend.DLL.Interface
{
    public interface IScheduleRepository
    {
        public Task<Schedule> GetSchedule(Guid Id);
        public Task<ReportSchedule> GetReportSchedule(Guid scheduleId);
        public Task<List<ReportScheduleRecipients>> GetReportScheduleRecipients(Guid reportScheduleId);
        public Task<NotificationSchedule> GetNotificationSchedule(Guid scheduleId);
        public Task<List<NotificationScheduledOns>> GetNotificationScheduledOns(Guid notificationScheduleId);
        public Task<List<NotificationTimes>> GetNotificationTimes(Guid notificationScheduleId); 
    }
}
