using GulfHrBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace GulfHrBackend.DLL
{
    public class AppDBContext:DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options):base(options) {

        }
        public DbSet<EmailTemplate> EmailTemplates { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<NotificationSchedule> NotificationSchedules { get; set; }
        public DbSet<ReportSchedule> ReportSchedules { get; set; }
        public DbSet<ReportScheduleRecipients> ReportScheduleRecipients { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<NotificationTimes> NotificationTimes { get; set; }
        public DbSet<NotificationScheduledOns> NotificationScheduledOns { get; set;}
    }
}
