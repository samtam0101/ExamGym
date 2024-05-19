using Domain.DTOs.TrainerDto;
using Domain.Filters;
using Domain.Responses;

namespace Infrastructure.Services.TrainerServices;

public interface ITrainerService
{
    Task<PagedResponse<List<GetTrainersDto>>> GetTrainersAsync(TrainerFilter filter);
    Task<Response<GetTrainersDto>> GetTrainerByIdAsync(int id);
    Task<Response<string>> AddTrainerAsync(AddTrainerDto addTrainer);
    Task<Response<string>> UpdateTrainerAsync(UpdateTrainerDto updateTrainer);
    Task<Response<bool>> DeleteTrainerAsync(int id);
}
