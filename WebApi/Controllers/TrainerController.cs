using Domain.DTOs.TrainerDto;
using Domain.Filters;
using Domain.Responses;
using Infrastructure.Services.TrainerServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;
[ApiController]
[Route("[controller]")]
public class TrainerController(ITrainerService trainerService):ControllerBase
{
    [HttpGet]
    [AllowAnonymous]
    public async Task<Response<List<GetTrainersDto>>> GetTrainersAsync([FromQuery]TrainerFilter TrainerFilter)
        => await trainerService.GetTrainersAsync(TrainerFilter);

    [HttpGet("{TrainerId:int}")]
    [AllowAnonymous]
    public async Task<Response<GetTrainersDto>> GetTrainerByIdAsync(int TrainerId)
        => await trainerService.GetTrainerByIdAsync(TrainerId);

    [HttpPost("create")]
    public async Task<Response<string>> CreateTrainerAsync([FromForm]AddTrainerDto Trainer)
        => await trainerService.AddTrainerAsync(Trainer);


    [HttpPut("update")]
    public async Task<Response<string>> UpdateTrainerAsync([FromForm]UpdateTrainerDto Trainer)
        => await trainerService.UpdateTrainerAsync(Trainer);

    [HttpDelete("{TrainerId:int}")]
    public async Task<Response<bool>> DeleteTrainerAsync(int TrainerId)
        => await trainerService.DeleteTrainerAsync(TrainerId);
}
