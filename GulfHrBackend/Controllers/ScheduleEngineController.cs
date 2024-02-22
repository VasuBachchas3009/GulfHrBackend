using GulfHrBackend.BLL.Interface;
using GulfHrBackend.Core.DTO;
using GulfHrBackend.DLL.DTO;
using GulfHrBackend.DLL.DTO.CreateScheduleDtos;
using GulfHrBackend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;



namespace GulfHrBackend.Controllers
{
    [Route("api")]
    [ApiController]
    public class ScheduleEngineController : ControllerBase
    {
        //eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJKV1RTZXJ2aWNlQWNjZXNzVG9rZW4iLCJMb2dnZWRJblVzZXIiOiJFMDI5MEFDQS0zNTY5LTQwRTgtQkVFMy0xRUE1QUYxN0I5QUEiLCJDb21wYW55SWQiOiJFMDI5MEFDQS0zNTY5LTQwRTgtQkVFMy0xRUE1QUYxN0I5QUEiLCJUZW5hbnRJZCI6IkUwMjkwQUNBLTM1NjktNDBFOC1CRUUzLTFFQTVBRjE3QjlBQSIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6InNlbGxlciIsImV4cCI6MTcwODQwODUzOSwiaXNzIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NzA0MyIsImF1ZCI6Imh0dHBzOi8vbG9jYWxob3N0OjcwNDMifQ.ugXi74AkDizgB0qR6d6E7DnHR3E_4LMixkiSo_SUPPg
        //"LoggedInUser":"E0290ACA-3569-40E8-BEE3-1EA5AF17B9AA",
        //"CompanyId":"E0290ACA-3569-40E8-BEE3-1EA5AF17B9AA",
        //"TenantId":"E0290ACA-3569-40E8-BEE3-1EA5AF17B9AA",
        private readonly IScheduleService _scheduleService;
        private readonly IConfiguration _configuration;

        public ScheduleEngineController(IScheduleService scheduleService,IConfiguration configuration)
        {
            _scheduleService = scheduleService;
            _configuration = configuration;
        }
        [HttpGet("schedules/{ScheduleId}")]
        public async Task<IActionResult> GetSchedule(Guid ScheduleId) {


            var schedule = await _scheduleService.GetSchedule(ScheduleId);
            return Ok(schedule);
        }
        [Authorize]
        [HttpPost("schedules")]
        public async Task<IActionResult> CreateSchedule(CreateScheduleDto createScheduleDto)
        {
            
            string createdBy = User.FindFirst("LoggedInUser")?.Value;
            string companyId = User.FindFirst("CompanyId")?.Value;
            string tenantId = User.FindFirst("TenantId")?.Value;
            if (createdBy == null || companyId == null || tenantId == null)
                throw new Exception("Authentication Error");
            CustomResponseDto<ResponseScheduleDto> response = await _scheduleService.AddSchedule(createScheduleDto, Guid.Parse(companyId), Guid.Parse(createdBy), Guid.Parse(tenantId));

            return StatusCode(response.Code,response);
;       }
        [HttpGet("generateToken")]
        public async Task<IActionResult> generateToken()
        {
            var claims = new List<Claim> {
                        new Claim(JwtRegisteredClaimNames.Sub,_configuration["Jwt:Subject"]),
                new Claim("LoggedInUser","E0290ACA-3569-40E8-BEE3-1EA5AF17B9AA"),
                new Claim("CompanyId","E0290ACA-3569-40E8-BEE3-1EA5AF17B9AA"),
                new Claim("TenantId","E0290ACA-3569-40E8-BEE3-1EA5AF17B9AA")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddMinutes(10),
                signingCredentials: signIn
                );
            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
            return Ok(jwtToken);
        }
        
        private string? FetchLoggedInUser()
        {
            return User.FindFirst("UserId")?.Value;
        }
    }
}
