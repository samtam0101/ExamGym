using System.Net;
using AutoMapper;
using Domain.DTOs.ClassScheduleDto;
using Domain.Entities;
using Domain.Filters;
using Domain.Responses;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.ClassScheduleServices;

public class ClassScheduleService(DataContext context, IMapper mapper) : IClassScheduleService
{
    public async Task<Response<string>> AddClassScheduleAsync(AddClassScheduleDto addClassScheduleDto)
    {
        try
            {
                var mapped = mapper.Map<ClassSchedule>(addClassScheduleDto); // Update entity reference
                await context.ClassSchedules.AddAsync(mapped); // Update entity reference
                await context.SaveChangesAsync();
                return new Response<string>("Successfully created "); // Update message
            }
            catch (Exception e)
            {
                return new Response<string>(HttpStatusCode.InternalServerError, e.Message);
            }
    }

    public async Task<Response<bool>> DeleteClassScheduleAsync(int id)
    {
        try
        {
            var existing = await context.ClassSchedules.FirstOrDefaultAsync(x => x.Id == id);
            if (existing == null) return new Response<bool>(HttpStatusCode.BadRequest, "Not Found");
            context.ClassSchedules.Remove(existing);
            await context.SaveChangesAsync();
            return new Response<bool>(true);
        }
        catch (Exception e)
        {
            return new Response<bool>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<GetClassSchedulesDto>> GetClassScheduleById(int id)
    {
        try
        {
            var existing = await context.ClassSchedules.FirstOrDefaultAsync(x => x.Id == id);
            if (existing == null) return new Response<GetClassSchedulesDto>(HttpStatusCode.BadRequest, "ClassSchedule not found");
            var product = mapper.Map<GetClassSchedulesDto>(existing);
            return new Response<GetClassSchedulesDto>(product);
        }
        catch (Exception e)
        {
            return new Response<GetClassSchedulesDto>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<PagedResponse<List<GetClassSchedulesDto>>> GetClassSchedulesAsync(ClassScheduleFilter filter)
    {
        try
            {
                var classSchedule = context.ClassSchedules.AsQueryable();
                if (!string.IsNullOrEmpty(filter.Location)) 
                    classSchedule = classSchedule.Where(x => x.Location.ToLower().Contains(filter.Location.ToLower())); 
                var result = await classSchedule.Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize)
                    .ToListAsync();
                var total = await classSchedule.CountAsync();

                var response = mapper.Map<List<GetClassSchedulesDto>>(result);
                return new PagedResponse<List<GetClassSchedulesDto>>(response, total, filter.PageNumber, filter.PageSize);
            }
            catch (Exception e)
            {
                return new PagedResponse<List<GetClassSchedulesDto>>(HttpStatusCode.InternalServerError, e.Message);
            }
    }

    public async Task<Response<string>> UpdateClassScheduleAsync(UpdateClassScheduleDto updateClassScheduleDto)
    {
        try
        {
            var existing = await context.ClassSchedules.AnyAsync(e => e.Id == updateClassScheduleDto.Id);
            if (!existing) return new Response<string>(HttpStatusCode.BadRequest, "ClassSchedule not found!");
            var mapped = mapper.Map<ClassSchedule>(updateClassScheduleDto);
            context.ClassSchedules.Update(mapped);
            await context.SaveChangesAsync();
            return new Response<string>("Updated successfully");
        }
        catch (Exception ex)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }
}
