using System.Net;
using Domain.DTOs;
using Domain.Responses;
using Infrastructure.Services.AccountServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;
[ApiController]
[Route("[controller]")]
[Authorize]
public class AccountController(IAccountService accountService):ControllerBase
{
    [HttpPost("Register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register([FromBody]RegisterDto registerDto)
    {
        if (ModelState.IsValid)
        {
            var response  = await accountService.Register(registerDto);
            return StatusCode((int)response.StatusCode, response);
        }
        else
        {
            var errorMessages = ModelState.SelectMany(e => e.Value.Errors.Select(e => e.ErrorMessage)).ToList();
            var response = new Response<RegisterDto>(HttpStatusCode.BadRequest, errorMessages);
            return StatusCode((int)response.StatusCode, response);
        }
        
    }
    
    [HttpPost("Login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody]LoginDto registerDto)
    {
        if (ModelState.IsValid)
        {
            var response  = await accountService.Login(registerDto);
            return StatusCode((int)response.StatusCode, response);
        }
        else
        {
            var errorMessages = ModelState.SelectMany(e => e.Value.Errors.Select(e => e.ErrorMessage)).ToList();
            var response = new Response<RegisterDto>(HttpStatusCode.BadRequest, errorMessages);
            return StatusCode((int)response.StatusCode, response);
        }
    }
}
