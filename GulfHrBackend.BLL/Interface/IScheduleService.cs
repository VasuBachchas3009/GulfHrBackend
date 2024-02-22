using GulfHrBackend.Core.DTO;
using GulfHrBackend.DLL.DTO;
using GulfHrBackend.DLL.DTO.CreateScheduleDtos;
using GulfHrBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GulfHrBackend.BLL.Interface
{
    public interface IScheduleService
    {
        public Task<ScheduleDTO> GetSchedule(Guid id);
        public Task<CustomResponseDto<ResponseScheduleDto>> AddSchedule(CreateScheduleDto createScheduleDto, Guid companyId, Guid createdBy, Guid tenantId);
    }
}
