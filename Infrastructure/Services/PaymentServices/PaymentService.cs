using System.Net;
using AutoMapper;
using Domain.DTOs.PaymentDto;
using Domain.Entities;
using Domain.Filters;
using Domain.Responses;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.PaymentServices;

public class PaymentService(DataContext context, IMapper mapper):IPaymentService
{
    public async Task<Response<string>> AddPaymentAsync(AddPaymentDto addPaymentDto)
    {
        try
        {
            var mapped = mapper.Map<Payment>(addPaymentDto);
            await context.Payments.AddAsync(mapped);
            await context.SaveChangesAsync();
            return new Response<string>("Successfully created ");
        }
        catch (Exception e)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<bool>> DeletePaymentAsync(int id)
    {
        try
        {
            var existing = await context.Payments.FirstOrDefaultAsync(x => x.Id == id);
            if (existing == null) return new Response<bool>(HttpStatusCode.BadRequest, "Not Found");
            context.Payments.Remove(existing);
            await context.SaveChangesAsync();
            return new Response<bool>(true);
        }
        catch (Exception e)
        {
            return new Response<bool>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<GetPaymentsDto>> GetPaymentById(int id)
    {
        try
        {
            var existing = await context.Payments.FirstOrDefaultAsync(x => x.Id == id);
            if (existing == null) return new Response<GetPaymentsDto>(HttpStatusCode.BadRequest, "Payment not found");
            var product = mapper.Map<GetPaymentsDto>(existing);
            return new Response<GetPaymentsDto>(product);
        }
        catch (Exception e)
        {
            return new Response<GetPaymentsDto>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<PagedResponse<List<GetPaymentsDto>>> GetPaymentsAsync(PaymentFilter filter)
    {
        try
        {
            var Payment = context.Payments.AsQueryable();
            if (!string.IsNullOrEmpty(filter.Status))
                Payment = Payment.Where(x => x.Status.ToLower().Contains(filter.Status.ToLower()));
            var result = await Payment.Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize)
                .ToListAsync();
            var total = await Payment.CountAsync();

            var response = mapper.Map<List<GetPaymentsDto>>(result);
            return new PagedResponse<List<GetPaymentsDto>>(response, total, filter.PageNumber, filter.PageSize);
        }
        catch (Exception e)
        {
            return new PagedResponse<List<GetPaymentsDto>>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<string>> UpdatePaymentAsync(UpdatePaymentDto updatePaymentDto)
    {
        try
        {
            var existing = await context.Payments.AnyAsync(e => e.Id == updatePaymentDto.Id);
            if (!existing) return new Response<string>(HttpStatusCode.BadRequest, "Payment not found!");
            var mapped = mapper.Map<Payment>(updatePaymentDto);
            context.Payments.Update(mapped);
            await context.SaveChangesAsync();
            return new Response<string>("Updated successfully");
        }
        catch (Exception ex)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }
}
