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
        private readonly AppDBContext _appDBContext;

        public ScheduleRepository(AppDBContext appDBContext)
        {
            _appDBContext = appDBContext;
        }

        public async Task AddRecipient(ReportScheduleRecipients recipient)
        {
            await _appDBContext.ReportScheduleRecipients.AddAsync(recipient);
            await _appDBContext.SaveChangesAsync();
        }

        public async Task<ReportSchedule> AddReportSchedule(ReportSchedule reportSchedule)
        {
            await _appDBContext.AddAsync(reportSchedule);
            await _appDBContext.SaveChangesAsync();
            return reportSchedule;
        }

        public async Task<Schedule> AddSchedule(Schedule schedule)
        {
            await _appDBContext.Schedules.AddAsync(schedule);
            await _appDBContext.SaveChangesAsync();
            return schedule;
        }

        public async Task<NotificationSchedule> GetNotificationSchedule(Guid scheduleId)
        {
            var notificationSchedule = await _appDBContext.NotificationSchedules.FirstOrDefaultAsync(x=>x.ScheduleId==scheduleId);
            return notificationSchedule;
        }

        public async Task<List<NotificationScheduledOns>> GetNotificationScheduledOns(Guid notificationScheduleId)
        {
            var notificationScheduleOns = await _appDBContext.NotificationScheduledOns.Where(x => x.NotificationScheduleId == notificationScheduleId).ToListAsync();
            return notificationScheduleOns;
        }

        public Task<List<NotificationTimes>> GetNotificationTimes(Guid notificationScheduleId)
        {
            var notificationTimes = _appDBContext.NotificationTimes.Where(x => x.NotificationScheduleId == notificationScheduleId).ToListAsync();
            return notificationTimes;
        }

        public async Task<ReportSchedule> GetReportSchedule(Guid scheduleId)
        {
            ReportSchedule reportSchedule = await _appDBContext.ReportSchedules.FirstOrDefaultAsync(x=>x.ScheduleId == scheduleId);
            return reportSchedule;
        }

        public Task<List<ReportScheduleRecipients>> GetReportScheduleRecipients(Guid scheduleId)
        {
            var recipients=_appDBContext.ReportScheduleRecipients.Where(x=>x.ScheduleId == scheduleId).ToListAsync();  
            return recipients;
        }

        public async Task<Schedule> GetSchedule(Guid Id)
        {

            Schedule? schedule = await _appDBContext.Schedules
                .FirstOrDefaultAsync(x=>x.ScheduleId==Id);

            if (schedule == null)
            {
                throw new Exception("The Requested Resource Was Not Found");
            }
            return schedule;
        }
    }
}
