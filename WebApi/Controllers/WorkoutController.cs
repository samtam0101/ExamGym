using Domain.DTOs.WorkoutDto;
using Domain.Filters;
using Domain.Responses;
using Infrastructure.Services.WorkoutServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;
[ApiController]
[Route("[controller]")]
[Authorize]
public class WorkoutController(IWorkoutService workoutService):ControllerBase
{
    [HttpGet("Get-Workouts")]
    public async Task<PagedResponse<List<GetWorkoutsDto>>> GetWorkoutsAsync([FromQuery]WorkoutFilter filter)
    {
        return await workoutService.GetWorkoutsAsync(filter);
    }
    [HttpPost("Add-Workout")]
    public async Task<Response<string>> AddWorkoutAsync(AddWorkoutDto addWorkoutDto)
    {
        return await workoutService.AddWorkoutAsync(addWorkoutDto);
    }
    [HttpPut("Update-Workout")]
    public async Task<Response<string>> UpdateWorkoutAsync(UpdateWorkoutDto updateWorkoutDto)
    {
        return await workoutService.UpdateWorkoutAsync(updateWorkoutDto);
    }
    [HttpGet("Get-WorkoutsById")]
    public async Task<Response<GetWorkoutsDto>> GetWorkoutsByIdAsync(int id)
    {
        return await workoutService.GetWorkoutById(id);
    }
    [HttpDelete("Delete-Workout")]
    public async Task<Response<bool>> DeleteWorkoutAsync(int id)
    {
        return await workoutService.DeleteWorkoutAsync(id);
    }
}
