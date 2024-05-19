using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using Domain.DTOs;
using Domain.Responses;
using Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Services.AccountServices;

public class AccountService(DataContext context, IConfiguration configuration, RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager) : IAccountService
{
    private async Task<string> GenerateJwtToken(IdentityUser user)
    {
        var key = Encoding.UTF8.GetBytes(configuration["Jwt:Key"]);
        var securityKey = new SymmetricSecurityKey(key);
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var claims = new List<Claim>()
        {
            new Claim(JwtRegisteredClaimNames.Name, user.UserName),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.NameId, user.Id),
        };
        
        //add roles
        var roles = await userManager.GetRolesAsync(user);
        claims.AddRange(roles.Select(role=>new Claim(ClaimTypes.Role,role)));
        
        var token = new JwtSecurityToken(
            issuer: configuration["Jwt:Issuer"],
            audience: configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: credentials
        );

       
        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
        return tokenString;
    }
    public async Task<Response<string>> Login(LoginDto loginDto)
    {
        var user = await userManager.FindByNameAsync(loginDto.Name);
        if ( user != null)
        {
            var checkPassword = await userManager.CheckPasswordAsync(user, loginDto.Password);
            if ( checkPassword ==  true )
            {
                var token = await GenerateJwtToken(user);
                return new Response<string>(token);
            }
            else{
                return new Response<string>(HttpStatusCode.BadRequest, "login or password is incorrect");
            }
        }
        return new Response<string>(HttpStatusCode.BadRequest, "login or password is incorrect");
    }

    public async Task<Response<RegisterDto>> Register(RegisterDto registerDto)
    {
        var mapped = new IdentityUser()
        {
            UserName = registerDto.Name,
            PasswordHash = registerDto.Password,
            Email = registerDto.Email
        };
        var response = await userManager.CreateAsync(mapped, registerDto.Password);
        if (response.Succeeded == true)
            return new Response<RegisterDto>(registerDto);
        else return new Response<RegisterDto>(HttpStatusCode.BadRequest, "something is wrong");
    }
}
