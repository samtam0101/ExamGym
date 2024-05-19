using Domain.DTOs.MembershipDto;
using Domain.Filters;
using Domain.Responses;
using Infrastructure.Services.MembershipServices;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;
[ApiController]
[Route("[controller]")]
public class MembershipController(IMembershipService membershipService):ControllerBase
{
    [HttpGet("Get-Memberships")]
    public async Task<PagedResponse<List<GetMembershipsDto>>> GetMembershipsAsync([FromQuery]MembershipFilter filter)
    {
        return await membershipService.GetMembershipsAsync(filter);
    }
    [HttpPost("Add-Membership")]
    public async Task<Response<string>> AddMembershipAsync(AddMembershipDto addMembershipDto)
    {
        return await membershipService.AddMembershipAsync(addMembershipDto);
    }
    [HttpPut("Update-Membership")]
    public async Task<Response<string>> UpdateMembershipAsync(UpdateMembershipDto updateMembershipDto)
    {
        return await membershipService.UpdateMembershipAsync(updateMembershipDto);
    }
    [HttpGet("Get-MembershipsById")]
    public async Task<Response<GetMembershipsDto>> GetMembershipsByIdAsync(int id)
    {
        return await membershipService.GetMembershipById(id);
    }
    [HttpDelete("Delete-Membership")]
    public async Task<Response<bool>> DeleteMembershipAsync(int id)
    {
        return await membershipService.DeleteMembershipAsync(id);
    }
}
