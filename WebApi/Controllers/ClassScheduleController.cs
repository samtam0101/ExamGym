using Domain.DTOs.ClassScheduleDto;
using Domain.Filters;
using Domain.Responses;
using Infrastructure.Services.ClassScheduleServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;
[ApiController]
[Route("[controller]")]
//[Authorize]
public class ClassScheduleController(IClassScheduleService classScheduleService):ControllerBase
{
    [HttpGet("Get-ClassSchedules")]
    public async Task<PagedResponse<List<GetClassSchedulesDto>>> GetClassSchedulesAsync([FromQuery]ClassScheduleFilter filter)
    {
        return await classScheduleService.GetClassSchedulesAsync(filter);
    }
    [HttpPost("Add-ClassSchedule")]
    public async Task<Response<string>> AddClassScheduleAsync(AddClassScheduleDto addClassScheduleDto)
    {
        return await classScheduleService.AddClassScheduleAsync(addClassScheduleDto);
    }
    [HttpPut("Update-ClassSchedule")]
    public async Task<Response<string>> UpdateClassScheduleAsync(UpdateClassScheduleDto updateClassScheduleDto)
    {
        return await classScheduleService.UpdateClassScheduleAsync(updateClassScheduleDto);
    }
    [HttpGet("Get-ClassSchedulesById")]
    public async Task<Response<GetClassSchedulesDto>> GetClassSchedulesByIdAsync(int id)
    {
        return await classScheduleService.GetClassScheduleById(id);
    }
    [HttpDelete("Delete-ClassSchedule")]
    public async Task<Response<bool>> DeleteClassScheduleAsync(int id)
    {
        return await classScheduleService.DeleteClassScheduleAsync(id);
    }
}
