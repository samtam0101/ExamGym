using System.Net;
using AutoMapper;
using Domain.DTOs.WorkoutDto;
using Domain.Entities;
using Domain.Filters;
using Domain.Responses;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.WorkoutServices;
public class WorkoutService(DataContext context, IMapper mapper):IWorkoutService
{
    public async Task<Response<string>> AddWorkoutAsync(AddWorkoutDto addWorkoutDto)
    {
        try
        {
            var mapped = mapper.Map<Workout>(addWorkoutDto);
            await context.Workouts.AddAsync(mapped);
            await context.SaveChangesAsync();
            return new Response<string>("Successfully created ");
        }
        catch (Exception e)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<bool>> DeleteWorkoutAsync(int id)
    {
        try
        {
            var existing = await context.Workouts.FirstOrDefaultAsync(x => x.Id == id);
            if (existing == null) return new Response<bool>(HttpStatusCode.BadRequest, "Not Found");
            context.Workouts.Remove(existing);
            await context.SaveChangesAsync();
            return new Response<bool>(true);
        }
        catch (Exception e)
        {
            return new Response<bool>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<GetWorkoutsDto>> GetWorkoutById(int id)
    {
        try
        {
            var existing = await context.Workouts.FirstOrDefaultAsync(x => x.Id == id);
            if (existing == null) return new Response<GetWorkoutsDto>(HttpStatusCode.BadRequest, "Workout not found");
            var product = mapper.Map<GetWorkoutsDto>(existing);
            return new Response<GetWorkoutsDto>(product);
        }
        catch (Exception e)
        {
            return new Response<GetWorkoutsDto>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<PagedResponse<List<GetWorkoutsDto>>> GetWorkoutsAsync(WorkoutFilter filter)
    {
        try
        {
            var Workout = context.Workouts.AsQueryable();
            if (!string.IsNullOrEmpty(filter.Title))
                Workout = Workout.Where(x => x.Title.ToLower().Contains(filter.Title.ToLower()));
            var result = await Workout.Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize)
                .ToListAsync();
            var total = await Workout.CountAsync();

            var response = mapper.Map<List<GetWorkoutsDto>>(result);
            return new PagedResponse<List<GetWorkoutsDto>>(response, total, filter.PageNumber, filter.PageSize);
        }
        catch (Exception e)
        {
            return new PagedResponse<List<GetWorkoutsDto>>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<string>> UpdateWorkoutAsync(UpdateWorkoutDto updateWorkoutDto)
    {
        try
        {
            var existing = await context.Workouts.AnyAsync(e => e.Id == updateWorkoutDto.Id);
            if (!existing) return new Response<string>(HttpStatusCode.BadRequest, "Workout not found!");
            var mapped = mapper.Map<Workout>(updateWorkoutDto);
            context.Workouts.Update(mapped);
            await context.SaveChangesAsync();
            return new Response<string>("Updated successfully");
        }
        catch (Exception ex)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }
}
