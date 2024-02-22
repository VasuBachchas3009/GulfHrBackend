using GulfHrBackend.BLL.Interface;
using GulfHrBackend.Core.DTO;
using GulfHrBackend.Core.Utility.Enums;
using GulfHrBackend.DLL.DTO;
using GulfHrBackend.DLL.DTO.CreateScheduleDtos;
using GulfHrBackend.DLL.Interface;
using GulfHrBackend.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
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

        public async Task<CustomResponseDto<ResponseScheduleDto>> AddSchedule(CreateScheduleDto createScheduleDto,Guid companyId, Guid createdBy, Guid tenantId)
        {
            try
            {
                //adding the schedule
                Schedule schedule = new Schedule();
                schedule.Name = createScheduleDto.Name;

                schedule.Type = GetSchedulerType(createScheduleDto.Type);
                schedule.ModuleId = createScheduleDto.ModuleId;
                schedule.EmailTemplateId = createScheduleDto.EmailTemplateId;
                schedule.IsActive = true;
                schedule.CreatedBy = createdBy;
                schedule.CompanyId = companyId;
                schedule.CreatedDate = DateTime.Now;
                schedule.TenantId = tenantId;
                schedule.DeletedBy = null;
                schedule.DeletedDate = null;
                schedule.IsDeleted = false;

                schedule.IsActive = true;
                schedule.LastUpdatedBy = createdBy;
                schedule.LastUpdatedDate = DateTime.Now;
                schedule.ModuleId = createScheduleDto.ModuleId;

                schedule = await scheduleRepository.AddSchedule(schedule);

                //adding reportSchedule
                CreateReportScheduleDto createReportScheduleDto = createScheduleDto.ReportSchedule;
                ReportSchedule reportSchedule = new ReportSchedule();
                reportSchedule.CreatedBy = createdBy;
                reportSchedule.CreatedDate = DateTime.Now;
                reportSchedule.DeletedBy = null;
                reportSchedule.DeletedDate = null;
                reportSchedule.IsDeleted = false;
                reportSchedule.EndsOn = GetSchedulerEndsOnValue(createReportScheduleDto.EndsOn);
                if (reportSchedule.EndsOn == SchedulerEndsOn.Never)
                {
                    reportSchedule.EndsOnDate = null;
                    reportSchedule.EndsOnAfter = null;
                }
                else if (reportSchedule.EndsOn == SchedulerEndsOn.On)
                {
                    reportSchedule.EndsOnDate = createReportScheduleDto.EndsOnDate;
                    reportSchedule.EndsOnAfter = null;
                }
                else
                {
                    if (createReportScheduleDto.EndsOnAfter <= 0)
                    {
                        throw new Exception("Invalid User Input");
                    }
                    reportSchedule.EndsOnDate = null;
                    reportSchedule.EndsOnAfter = createReportScheduleDto.EndsOnAfter;
                    
                }
                reportSchedule.ExportFileType = GetExportFileTypeValue(createReportScheduleDto.ExportFileType);
                reportSchedule.FromDate = createReportScheduleDto.FromDate;
                reportSchedule.LastUpdatedDate= DateTime.Now;
                reportSchedule.LastUpdatedBy = createdBy;
                reportSchedule.ReportId=Guid.NewGuid();
                reportSchedule.ScheduleId = schedule.ScheduleId;
                reportSchedule.ReportFrequency = GetReportFrequencyFrequency(createReportScheduleDto.Frequency);
                reportSchedule.RepeatOnDate = 0;
                
                if(reportSchedule.ReportFrequency == ReportFrequency.Daily)
                {
                    reportSchedule.RepeatOnDays = null;
                }
                else
                {
                    reportSchedule.RepeatOnDays = createReportScheduleDto.RepeatOnDays;
                }
                
                reportSchedule=await scheduleRepository.AddReportSchedule(reportSchedule);



                //Adding Recipients
                foreach(var recipient in createScheduleDto.Recipients)
                {
                    ReportScheduleRecipients reportScheduleRecipient = new ReportScheduleRecipients();  
                    reportScheduleRecipient.IsDeleted=false; 
                    reportScheduleRecipient.CreatedDate = DateTime.Now;
                    reportScheduleRecipient.CreatedBy= createdBy;
                    reportScheduleRecipient.DeletedDate = null;
                    reportScheduleRecipient.DeletedBy = null;
                    reportScheduleRecipient.ScheduleId = schedule.ScheduleId;
                    reportScheduleRecipient.UserType = recipient.Type;
                    reportScheduleRecipient.Id = recipient.RecipientId;
                    reportScheduleRecipient.LastUpdatedBy= createdBy;
                    reportScheduleRecipient.LastUpdatedDate = DateTime.Now; 
                    await scheduleRepository.AddRecipient(reportScheduleRecipient);
                }
                ResponseScheduleDto responseScheduleDto = ReturnResponseSchedule(schedule,createScheduleDto, reportSchedule);
                CustomResponseDto<ResponseScheduleDto> responseDto = new CustomResponseDto<ResponseScheduleDto>();
                responseDto.Status = "Success";
                responseDto.Code = 201;
                responseDto.RequestId = new Guid();
                responseDto.Message = "Schedule Created";
                responseDto.data = responseScheduleDto;

                return responseDto;
            }
            catch (Exception ex)
            {
                throw new Exception("Invalid User Input");
            }
            
            
        }


        public ResponseScheduleDto ReturnResponseSchedule(Schedule schedule,CreateScheduleDto createScheduleDto,ReportSchedule reportSchedule)
        {
            ResponseScheduleDto returnResponseScheduleDto = new ResponseScheduleDto();
            returnResponseScheduleDto.Id = schedule.ScheduleId;
            returnResponseScheduleDto.IsDeleted = schedule.IsDeleted;
            returnResponseScheduleDto.CreatedBy = schedule.CreatedBy;
            returnResponseScheduleDto.CreatedDate = schedule.CreatedDate; 
            returnResponseScheduleDto.LastUpdatedBy = schedule.LastUpdatedBy;
            returnResponseScheduleDto.LastUpdatedDate = schedule.LastUpdatedDate;
            returnResponseScheduleDto.DeletedBy = schedule.DeletedBy;
            returnResponseScheduleDto.DeletedDate = schedule.DeletedDate;
            returnResponseScheduleDto.TenantId = schedule.TenantId;
            returnResponseScheduleDto.CompanyId = schedule.CompanyId;
            returnResponseScheduleDto.Name = schedule.Name;
            returnResponseScheduleDto.Type = schedule.Type.ToString();
            returnResponseScheduleDto.ModuleId = schedule.ModuleId;
            returnResponseScheduleDto.EmailTemplateId = schedule.EmailTemplateId;
            returnResponseScheduleDto.IsActive = schedule.IsActive;
            List<ResponseRecipientDto> responseRecipients = new List<ResponseRecipientDto>();   
            foreach (var recipient in createScheduleDto.Recipients)
            {
                ResponseRecipientDto recipientDto = new ResponseRecipientDto();
                recipientDto.RecipientId = recipient.RecipientId;
                recipientDto.Id = recipient.RecipientId;
                recipientDto.ScheduleId=schedule.ScheduleId;
                recipientDto.Type= recipient.Type;
                responseRecipients.Add(recipientDto);
            }

            returnResponseScheduleDto.NotificationSchedule = null;
            returnResponseScheduleDto.Recipients= responseRecipients;
            ResponseReportScheduleDto responseReportScheduleDto = new ResponseReportScheduleDto();
            responseReportScheduleDto.Id = reportSchedule.ReportScheduleId;
            responseReportScheduleDto.ScheduleId = schedule.ScheduleId;
            responseReportScheduleDto.ReportId = reportSchedule.ReportId;
            responseReportScheduleDto.FromDate = reportSchedule.FromDate;
            responseReportScheduleDto.Frequency = reportSchedule.ReportFrequency.ToString();
            responseReportScheduleDto.RepeatOnDays = reportSchedule.RepeatOnDays;
            responseReportScheduleDto.RepeatOnDate = reportSchedule.RepeatOnDate;
            responseReportScheduleDto.EndsOn = reportSchedule.EndsOn.ToString();
            responseReportScheduleDto.EndsOnDate = reportSchedule.EndsOnDate;
            responseReportScheduleDto.EndsOnAfter = reportSchedule.EndsOnAfter;
            responseReportScheduleDto.ExportFileType = reportSchedule.ExportFileType.ToString();
            returnResponseScheduleDto.ReportSchedule = responseReportScheduleDto;

            return returnResponseScheduleDto;
        }
        private ReportFrequency GetReportFrequencyFrequency(string frequency)
        {
            if(frequency==ReportFrequency.Weekly.ToString()) return ReportFrequency.Weekly;
            else if (frequency == ReportFrequency.Yearly.ToString()) return ReportFrequency.Yearly;
            else if (frequency == ReportFrequency.Daily.ToString()) return ReportFrequency.Daily;
            else if (frequency == ReportFrequency.Monthly.ToString()) return ReportFrequency.Monthly;
            else throw new Exception("Invalid User Input");
        }
        private ExportFileType GetExportFileTypeValue(string fileType)
        {
            if(fileType ==ExportFileType.CSV.ToString())
            {
                return ExportFileType.CSV;
            }
            else if(fileType ==ExportFileType.XLS.ToString()) 
            {
                return ExportFileType.XLS; 
            }
            else throw new Exception("Invalid User Input");
        }
        private SchedulerEndsOn GetSchedulerEndsOnValue(string endsOn)
        {
            if (endsOn == SchedulerEndsOn.On.ToString()) return SchedulerEndsOn.On;
            else if (endsOn == SchedulerEndsOn.After.ToString()) return SchedulerEndsOn.After;
            else if (endsOn == SchedulerEndsOn.Never.ToString()) return SchedulerEndsOn.Never;
            else throw new Exception("Invalid User Input");
        }
        private SchedulerType GetSchedulerType(string type)
        {
            if (type == SchedulerType.Report.ToString())
            {
                return SchedulerType.Report;
            }
            else if (type == SchedulerType.Expiry.ToString())
            {
                return SchedulerType.Expiry;
            }
            else
            {
                throw new Exception("Invalid User Input");
            }
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
            reportScheduleDto.EndsOnDate = reportSchedule.EndsOnDate.ToString();
            reportScheduleDto.EndsOnAfter = reportSchedule.EndsOnAfter;

            List<ReportScheduleRecipients> recipients = await scheduleRepository.GetReportScheduleRecipients(id);
            List<ResponseRecipientDto> responseRecipientDtos = new List<ResponseRecipientDto>();
            foreach (ReportScheduleRecipients recipient in recipients)
            {
                ResponseRecipientDto responseRecipientDto = new ResponseRecipientDto(); 
                responseRecipientDto.ScheduleId=recipient.ScheduleId;
                responseRecipientDto.RecipientId = recipient.Id;
                responseRecipientDto.Id = recipient.Id;
                responseRecipientDto.Type = recipient.UserType;
                responseRecipientDtos.Add(responseRecipientDto);
            }
            reportScheduleDto.ReportSceduleRecipients = responseRecipientDtos;

            NotificationScheduleDto notificationScheduleDto = null;
            NotificationSchedule notificationSchedule = await scheduleRepository.GetNotificationSchedule(id);
            if(notificationSchedule != null)
            {
                notificationScheduleDto = new NotificationScheduleDto();
                notificationScheduleDto.Id = notificationSchedule.NotificationScheduleId;
                notificationScheduleDto.ScheduleId = notificationSchedule.ScheduleId;
                notificationSchedule.NotificationScheduleId = notificationSchedule.NotificationScheduleId;
                notificationScheduleDto.Type = notificationSchedule.Type.ToString();
                notificationScheduleDto.TillEndDate = notificationSchedule.TillEndDate;
                notificationScheduleDto.IfHoliday = notificationSchedule.IfHoliday.ToString();
                List<NotificationScheduledOns> notificationScheduledOns = await scheduleRepository.GetNotificationScheduledOns(notificationSchedule.NotificationScheduleId);
                List<NotificationTimes> notificationTimes = await scheduleRepository.GetNotificationTimes(notificationSchedule.NotificationScheduleId);
                List<NotificationScheduledOnDto> notificationScheduledOnDtos = new List<NotificationScheduledOnDto>();
                foreach (var notificationScheduledOn in notificationScheduledOns)
                {
                    NotificationScheduledOnDto notificationScheduledOnDto = new NotificationScheduledOnDto();
                    notificationScheduledOnDto.NotificationScheduleId = notificationScheduledOn.NotificationScheduleId;
                    notificationScheduledOnDto.Id = notificationScheduledOn.Id;
                    notificationScheduledOnDto.Count = notificationScheduledOn.Count;
                    notificationScheduledOnDto.ScheduleOn = notificationScheduledOn.ScheduleOn.ToString();
                    notificationScheduledOnDtos.Add(notificationScheduledOnDto);
                }
                notificationScheduleDto.NotificationScheduledOns = notificationScheduledOnDtos;

                List<NotificationTimeDto> notificationTimeDtos = new List<NotificationTimeDto>();
                foreach (var notificationTime in notificationTimes)
                {
                    NotificationTimeDto notificationTimeDto = new NotificationTimeDto();
                    notificationTimeDto.NotificationScheduleId = notificationTime.NotificationScheduleId;
                    notificationTimeDto.Id = notificationTime.Id;
                    notificationTimeDto.Hour = notificationTime.Hour;
                    notificationTimeDto.Period = notificationTime.period.ToString();
                    notificationTimeDtos.Add(notificationTimeDto);
                }
                notificationScheduleDto.NotificationTimes = notificationTimeDtos;
            }
            
            scheduleDTO.NotificationSchedule = notificationScheduleDto;


            
            return scheduleDTO;  
        }
    }
}
