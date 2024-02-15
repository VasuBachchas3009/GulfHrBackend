using GulfHrBackend.DLL.DTO;
using GulfHrBackend.DLL.Interface;
using GulfHrBackend.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GulfHrBackend.DLL.Repository
{
    public class ScheduleRepository : IScheduleRepository
    {
        private readonly AppDBContext appDBContext;

        public ScheduleRepository(AppDBContext appDBContext)
        {
            this.appDBContext = appDBContext;
        }

        public async Task<NotificationSchedule> GetNotificationSchedule(Guid scheduleId)
        {
            var notificationSchedule = await appDBContext.NotificationSchedules.FirstOrDefaultAsync(x=>x.ScheduleId==scheduleId);
            return notificationSchedule;
        }

        public Task<List<NotificationScheduledOns>> GetNotificationScheduledOns(Guid notificationScheduleId)
        {
            var notificationScheduleOns = appDBContext.NotificationScheduledOns.Where(x => x.NotificationScheduleId == notificationScheduleId).ToListAsync();
            return notificationScheduleOns;
        }

        public Task<List<NotificationTimes>> GetNotificationTimes(Guid notificationScheduleId)
        {
            var notificationTimes = appDBContext.NotificationTimes.Where(x => x.NotificationScheduleId == notificationScheduleId).ToListAsync();
            return notificationTimes;
        }

        public async Task<ReportSchedule> GetReportSchedule(Guid scheduleId)
        {
            ReportSchedule reportSchedule = await appDBContext.ReportSchedules.FirstOrDefaultAsync(x=>x.ScheduleId == scheduleId);
            return reportSchedule;
        }

        public Task<List<ReportScheduleRecipients>> GetReportScheduleRecipients(Guid reportScheduleId)
        {
            var recipients=appDBContext.ReportScheduleRecipients.Where(x=>x.ReportScheduleId == reportScheduleId).ToListAsync();  
            return recipients;
        }

        public async Task<Schedule> GetSchedule(Guid Id)
        {

            Schedule schedule = await appDBContext.Schedules
                .Include(schedule=>schedule.ReportSchedule)
                .Include(schedule => schedule.NotificationSchedule)
                .FirstOrDefaultAsync(x=>x.ScheduleId==Id);

            if (schedule == null)
            {
                throw new Exception("The Requested Resource Was Not Found");
            }
            return schedule;
        }
    }
}
