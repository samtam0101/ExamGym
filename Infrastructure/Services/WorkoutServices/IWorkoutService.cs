using Domain.DTOs.WorkoutDto;
using Domain.Filters;
using Domain.Responses;

namespace Infrastructure.Services.WorkoutServices;

public interface IWorkoutService
{
    Task<Response<string>> AddWorkoutAsync(AddWorkoutDto addWorkoutDto);
    Task<Response<string>> UpdateWorkoutAsync(UpdateWorkoutDto updateWorkoutDto);
    Task<PagedResponse<List<GetWorkoutsDto>>> GetWorkoutsAsync(WorkoutFilter filter);
    Task<Response<bool>> DeleteWorkoutAsync(int id);
    Task<Response<GetWorkoutsDto>> GetWorkoutById(int id);
}
