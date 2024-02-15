using GulfHrBackend.DLL.DTO;
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
    }
}
