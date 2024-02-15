using GulfHrBackend.BLL.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GulfHrBackend.Controllers
{
    [Route("api")]
    [ApiController]
    public class ScheduleEngineController : ControllerBase
    {
        private readonly IScheduleService scheduleService;

        public ScheduleEngineController(IScheduleService scheduleService)
        {
            this.scheduleService = scheduleService;
        }
        [HttpGet("schedule/{ScheduleId}")]
        public async Task<IActionResult> GetSchedule(Guid ScheduleId) {
            
            
            var schedule=await scheduleService.GetSchedule(ScheduleId);
            return Ok(schedule);
        }
    }
}
