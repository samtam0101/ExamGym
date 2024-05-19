using Domain.DTOs.UserDto;
using Domain.Filters;
using Domain.Responses;

namespace Infrastructure.Services.UserServices;

public interface IUserService
{
    Task<Response<string>> AddUserAsync(AddUserDto addUserDto);
    Task<Response<string>> UpdateUserAsync(UpdateUserDto updateUserDto);
    Task<PagedResponse<List<GetUsersDto>>> GetUsersAsync(UserFilter filter);
    Task<Response<bool>> DeleteUserAsync(int id);
    Task<Response<GetUsersDto>> GetUserById(int id);
}