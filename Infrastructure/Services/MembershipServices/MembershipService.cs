using System.Net;
using AutoMapper;
using Domain.DTOs.MembershipDto;
using Domain.Entities;
using Domain.Filters;
using Domain.Responses;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.MembershipServices;

public class MembershipService(DataContext context, IMapper mapper):IMembershipService
{
     public async Task<Response<string>> AddMembershipAsync(AddMembershipDto addMembershipDto)
    {
        try
        {
            var mapped = mapper.Map<Membership>(addMembershipDto);
            await context.Memberships.AddAsync(mapped);
            await context.SaveChangesAsync();
            return new Response<string>("Successfully created ");
        }
        catch (Exception e)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<bool>> DeleteMembershipAsync(int id)
    {
        try
        {
            var existing = await context.Memberships.FirstOrDefaultAsync(x => x.Id == id);
            if (existing == null) return new Response<bool>(HttpStatusCode.BadRequest, "Not Found");
            context.Memberships.Remove(existing);
            await context.SaveChangesAsync();
            return new Response<bool>(true);
        }
        catch (Exception e)
        {
            return new Response<bool>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<GetMembershipsDto>> GetMembershipById(int id)
    {
        try
        {
            var existing = await context.Memberships.FirstOrDefaultAsync(x => x.Id == id);
            if (existing == null) return new Response<GetMembershipsDto>(HttpStatusCode.BadRequest, "Membership not found");
            var product = mapper.Map<GetMembershipsDto>(existing);
            return new Response<GetMembershipsDto>(product);
        }
        catch (Exception e)
        {
            return new Response<GetMembershipsDto>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<PagedResponse<List<GetMembershipsDto>>> GetMembershipsAsync(MembershipFilter filter)
    {
        try
        {
            var Membership = context.Memberships.AsQueryable();
            if (!string.IsNullOrEmpty(filter.Type))
                Membership = Membership.Where(x => x.Type.ToLower().Contains(filter.Type.ToLower()));
            var result = await Membership.Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize)
                .ToListAsync();
            var total = await Membership.CountAsync();

            var response = mapper.Map<List<GetMembershipsDto>>(result);
            return new PagedResponse<List<GetMembershipsDto>>(response, total, filter.PageNumber, filter.PageSize);
        }
        catch (Exception e)
        {
            return new PagedResponse<List<GetMembershipsDto>>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<string>> UpdateMembershipAsync(UpdateMembershipDto updateMembershipDto)
    {
        try
        {
            var existing = await context.Memberships.AnyAsync(e => e.Id == updateMembershipDto.Id);
            if (!existing) return new Response<string>(HttpStatusCode.BadRequest, "Membership not found!");
            var mapped = mapper.Map<Membership>(updateMembershipDto);
            context.Memberships.Update(mapped);
            await context.SaveChangesAsync();
            return new Response<string>("Updated successfully");
        }
        catch (Exception ex)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }
}
