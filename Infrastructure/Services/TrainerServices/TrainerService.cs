using Infrastructure.Data;
using Infrastructure.Services.FileService;

using System.Net;
using Domain.DTOs.TrainerDto;
using Domain.Entities;
using Domain.Filters;
using Domain.Responses;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.TrainerServices;

public class TrainerService(DataContext context, IFileService fileService) : ITrainerService
{

    public async Task<PagedResponse<List<GetTrainersDto>>> GetTrainersAsync(TrainerFilter filter)
    {
        try
        {
            var Trainers = context.Trainers.Include(x => x.ClassSchedules).AsQueryable();

            if (!string.IsNullOrEmpty(filter.Name))
                Trainers = Trainers.Where(x => x.Name.ToLower().Contains(filter.Name.ToLower()));


            var response = await Trainers.Select(x => new GetTrainersDto()
            {
                Name = x.Name,
                Email = x.Email,
                Phone = x.Phone,
                Specialization = x.Specialization,
                Photo = x.Photo,
                Id = x.Id,
            }).Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToListAsync();
            var totalRecord = await Trainers.CountAsync();

            return new PagedResponse<List<GetTrainersDto>>(response, totalRecord, filter.PageNumber, filter.PageSize);
        }
        catch (Exception e)
        {
            return new PagedResponse<List<GetTrainersDto>>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<GetTrainersDto>> GetTrainerByIdAsync(int TrainerId)
    {
        try
        {
            var existing = await context.Trainers.FirstOrDefaultAsync(x => x.Id == TrainerId);
            if (existing == null) return new Response<GetTrainersDto>(HttpStatusCode.BadRequest, "Not Found");
            var response = new GetTrainersDto()
            {
                Name = existing.Name,
                Email = existing.Email,
                Phone= existing.Phone,
                Specialization = existing.Specialization,
                Photo = existing.Photo,
                Id = existing.Id,
            };
            return new Response<GetTrainersDto>(response);
        }
        catch (Exception e)
        {
            return new Response<GetTrainersDto>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<string>> AddTrainerAsync(AddTrainerDto createTrainer)
    {
        try
        {
            var Trainer = new Trainer()
            {
                Name = createTrainer.Name,
                Email = createTrainer.Email,
                Specialization = createTrainer.Specialization,
                Phone = createTrainer.Phone,
                Photo = await fileService.CreateFile(createTrainer.Photo),
            };
            await context.Trainers.AddAsync(Trainer);
            await context.SaveChangesAsync();
            return new Response<string>("Successfully created Trainer");
        }
        catch (Exception e)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, e.Message);
        }
    }


    public async Task<Response<string>> UpdateTrainerAsync(UpdateTrainerDto updateTrainer)
    {
        try
        {
            var existingTrainer = await context.Trainers.FirstOrDefaultAsync(x => x.Id == updateTrainer.Id);
            if (existingTrainer == null) return new Response<string>(HttpStatusCode.BadRequest, "Trainer not found");

            if (updateTrainer.Photo != null)
            {
                fileService.DeleteFile(existingTrainer.Photo);
                existingTrainer.Photo = await fileService.CreateFile(updateTrainer.Photo);
            }

            existingTrainer.Name = updateTrainer.Name;
            existingTrainer.Email = updateTrainer.Email;
            existingTrainer.Phone = updateTrainer.Phone;
            existingTrainer.Specialization = updateTrainer.Specialization;
            existingTrainer.Id = updateTrainer.Id;

            await context.SaveChangesAsync();
            return new Response<string>("Successfully updated the trainer");
        }
        catch (Exception e)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<bool>> DeleteTrainerAsync(int id)
    {
        try
        {
            var existing = await context.Trainers.FirstOrDefaultAsync(x => x.Id == id);
            if (existing == null) return new Response<bool>(HttpStatusCode.BadRequest, "Not Found");
            fileService.DeleteFile(existing.Photo);
            context.Trainers.Remove(existing);
            await context.SaveChangesAsync();
            return new Response<bool>(true);
        }
        catch (Exception e)
        {
            return new Response<bool>(HttpStatusCode.InternalServerError, e.Message);
        }
    }
}