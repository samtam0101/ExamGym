using Domain.DTOs;
using Domain.Responses;

namespace Infrastructure.Services.AccountServices;

public interface IAccountService
{
    Task<Response<RegisterDto>> Register(RegisterDto registerDto);
    Task<Response<string>> Login(LoginDto loginDto);
}
