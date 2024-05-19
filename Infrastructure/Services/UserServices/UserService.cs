using System.Net;
using AutoMapper;
using Domain.DTOs.UserDto;
using Domain.Entities;
using Domain.Filters;
using Domain.Responses;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.UserServices;

public class UserService(DataContext context, IMapper mapper) : IUserService
{
    public async Task<Response<string>> AddUserAsync(AddUserDto addUserDto)
    {
        try
        {
            var mapped = mapper.Map<User>(addUserDto);
            await context.Users.AddAsync(mapped);
            await context.SaveChangesAsync();
            return new Response<string>("Successfully created ");
        }
        catch (Exception e)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<bool>> DeleteUserAsync(int id)
    {
        try
        {
            var existing = await context.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (existing == null) return new Response<bool>(HttpStatusCode.BadRequest, "Not Found");
            context.Users.Remove(existing);
            await context.SaveChangesAsync();
            return new Response<bool>(true);
        }
        catch (Exception e)
        {
            return new Response<bool>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<GetUsersDto>> GetUserById(int id)
    {
        try
        {
            var existing = await context.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (existing == null) return new Response<GetUsersDto>(HttpStatusCode.BadRequest, "User not found");
            var product = mapper.Map<GetUsersDto>(existing);
            return new Response<GetUsersDto>(product);
        }
        catch (Exception e)
        {
            return new Response<GetUsersDto>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<PagedResponse<List<GetUsersDto>>> GetUsersAsync(UserFilter filter)
    {
        try
        {
            var User = context.Users.AsQueryable();
            if (!string.IsNullOrEmpty(filter.Name))
                User = User.Where(x => x.Name.ToLower().Contains(filter.Name.ToLower()));
            var result = await User.Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize)
                .ToListAsync();
            var total = await User.CountAsync();

            var response = mapper.Map<List<GetUsersDto>>(result);
            return new PagedResponse<List<GetUsersDto>>(response, total, filter.PageNumber, filter.PageSize);
        }
        catch (Exception e)
        {
            return new PagedResponse<List<GetUsersDto>>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<string>> UpdateUserAsync(UpdateUserDto updateUserDto)
    {
        try
        {
            var existing = await context.Users.AnyAsync(e => e.Id == updateUserDto.Id);
            if (!existing) return new Response<string>(HttpStatusCode.BadRequest, "User not found!");
            var mapped = mapper.Map<User>(updateUserDto);
            context.Users.Update(mapped);
            await context.SaveChangesAsync();
            return new Response<string>("Updated successfully");
        }
        catch (Exception ex)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }
}
