using Domain.DTOs.UserDto;
using Domain.Filters;
using Domain.Responses;
using Infrastructure.Services.UserServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;
[ApiController]
[Route("[controller]")]
//[Authorize]
public class UserController(IUserService userService):ControllerBase
{
    [HttpGet("Get-Users")]
    public async Task<PagedResponse<List<GetUsersDto>>> GetUsersAsync([FromQuery]UserFilter filter)
    {
        return await userService.GetUsersAsync(filter);
    }
    [HttpPost("Add-User")]
    public async Task<Response<string>> AddUserAsync(AddUserDto addUserDto)
    {
        return await userService.AddUserAsync(addUserDto);
    }
    [HttpPut("Update-User")]
    public async Task<Response<string>> UpdateUserAsync(UpdateUserDto updateUserDto)
    {
        return await userService.UpdateUserAsync(updateUserDto);
    }
    [HttpGet("Get-UsersById")]
    public async Task<Response<GetUsersDto>> GetUsersByIdAsync(int id)
    {
        return await userService.GetUserById(id);
    }
    [HttpDelete("Delete-User")]
    public async Task<Response<bool>> DeleteUserAsync(int id)
    {
        return await userService.DeleteUserAsync(id);
    }
}
