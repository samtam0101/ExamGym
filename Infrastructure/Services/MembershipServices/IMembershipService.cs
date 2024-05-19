using Domain.DTOs.MembershipDto;
using Domain.Filters;
using Domain.Responses;

namespace Infrastructure.Services.MembershipServices;

public interface IMembershipService
{
    Task<Response<string>> AddMembershipAsync(AddMembershipDto addMembershipDto);
    Task<Response<string>> UpdateMembershipAsync(UpdateMembershipDto updateMembershipDto);
    Task<PagedResponse<List<GetMembershipsDto>>> GetMembershipsAsync(MembershipFilter filter);
    Task<Response<bool>> DeleteMembershipAsync(int id);
    Task<Response<GetMembershipsDto>> GetMembershipById(int id);
}
