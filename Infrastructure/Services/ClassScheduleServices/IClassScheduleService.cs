using Domain.DTOs.ClassScheduleDto;
using Domain.Filters;
using Domain.Responses;

namespace Infrastructure.Services.ClassScheduleServices;

public interface IClassScheduleService
{
    Task<Response<string>> AddClassScheduleAsync(AddClassScheduleDto addClassScheduleDto);
    Task<Response<string>> UpdateClassScheduleAsync(UpdateClassScheduleDto updateClassScheduleDto);
    Task<PagedResponse<List<GetClassSchedulesDto>>> GetClassSchedulesAsync(ClassScheduleFilter filter);
    Task<Response<bool>> DeleteClassScheduleAsync(int id);
    Task<Response<GetClassSchedulesDto>> GetClassScheduleById(int id);
}
