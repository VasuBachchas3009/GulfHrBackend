using GulfHrBackend.BLL.Interface;
using GulfHrBackend.DLL.DTO;
using GulfHrBackend.DLL.Interface;
using GulfHrBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GulfHrBackend.BLL.Service
{
    public class ScheduleService : IScheduleService
    {
        private readonly IScheduleRepository scheduleRepository;

        public ScheduleService(IScheduleRepository scheduleRepository)
        {
            this.scheduleRepository = scheduleRepository;
        }
        public async Task<ScheduleDTO> GetSchedule(Guid id)
        {
            var schedule=await scheduleRepository.GetSchedule(id);
            ScheduleDTO scheduleDTO = new ScheduleDTO();
            scheduleDTO.Id = id;
            scheduleDTO.Name = schedule.Name;
            scheduleDTO.Type=schedule.Type.ToString();
            scheduleDTO.ModuleId = schedule.ModuleId;
            scheduleDTO.EmailTemplateId = schedule.EmailTemplateId;
            scheduleDTO.IsActive = schedule.IsActive;
            ReportScheduleDto reportScheduleDto = new ReportScheduleDto();
            scheduleDTO.ReportSchedule=reportScheduleDto;
            ReportSchedule reportSchedule =await scheduleRepository.GetReportSchedule(id);
            reportScheduleDto.Id = reportSchedule.ReportScheduleId;
            reportScheduleDto.ScheduleId = reportSchedule.ScheduleId;   
            reportScheduleDto.ReportId = reportSchedule.ReportId;
            reportScheduleDto.FromDate=reportSchedule.FromDate.ToShortDateString(); 
            reportScheduleDto.ExportFileType=reportSchedule.ExportFileType.ToString();  
            reportScheduleDto.Frequency=reportSchedule.ReportFrequency.ToString();
            reportScheduleDto.EndsOn=reportSchedule.EndsOn.ToString();
            reportScheduleDto.EndsOnDate = reportSchedule.EndsOnDate.ToShortDateString();
            reportScheduleDto.EndsOnAfter = reportSchedule.EndsOnAfter;

            List<ReportScheduleRecipients> recipients = await scheduleRepository.GetReportScheduleRecipients(reportSchedule.ReportScheduleId);
            reportScheduleDto.ReportSceduleRecipients = recipients; 

            NotificationScheduleDto notificationScheduleDto = new NotificationScheduleDto();
            NotificationSchedule notificationSchedule = await scheduleRepository.GetNotificationSchedule(id);
            notificationScheduleDto.Id = notificationSchedule.NotificationScheduleId;   
            notificationScheduleDto.ScheduleId = notificationSchedule.ScheduleId;
            notificationSchedule.NotificationScheduleId = notificationSchedule.NotificationScheduleId;  
            notificationScheduleDto.Type=notificationSchedule.Type.ToString();
            notificationScheduleDto.TillEndDate = notificationSchedule.TillEndDate;
            notificationScheduleDto.IfHoliday=notificationSchedule.IfHoliday.ToString();
            scheduleDTO.NotificationSchedule = notificationScheduleDto;


            List<NotificationScheduledOns> notificationScheduledOns = await scheduleRepository.GetNotificationScheduledOns(notificationSchedule.NotificationScheduleId);
            List<NotificationTimes> notificationTimes = await scheduleRepository.GetNotificationTimes(notificationSchedule.NotificationScheduleId);
            List<NotificationScheduledOnDto> notificationScheduledOnDtos = new List<NotificationScheduledOnDto>();
            foreach(var notificationScheduledOn in notificationScheduledOns)
            {
                NotificationScheduledOnDto notificationScheduledOnDto = new NotificationScheduledOnDto();
                notificationScheduledOnDto.NotificationScheduleId=notificationScheduledOn.NotificationScheduleId;
                notificationScheduledOnDto.Id= notificationScheduledOn.Id;
                notificationScheduledOnDto.Count= notificationScheduledOn.Count;
                notificationScheduledOnDto.ScheduleOn = notificationScheduledOn.ScheduleOn.ToString();
                notificationScheduledOnDtos.Add(notificationScheduledOnDto);
            }
            notificationScheduleDto.NotificationScheduledOns = notificationScheduledOnDtos;

            List<NotificationTimeDto> notificationTimeDtos = new List<NotificationTimeDto>();   
            foreach(var notificationTime in notificationTimes)
            {
                NotificationTimeDto notificationTimeDto = new NotificationTimeDto();
                notificationTimeDto.NotificationScheduleId=notificationTime.NotificationScheduleId;
                notificationTimeDto.Id= notificationTime.Id;
                notificationTimeDto.Hour= notificationTime.Hour;
                notificationTimeDto.Period=notificationTime.period.ToString();
                notificationTimeDtos.Add(notificationTimeDto);
            }
            notificationScheduleDto.NotificationTimes= notificationTimeDtos;
            return scheduleDTO;  
        }
    }
}
