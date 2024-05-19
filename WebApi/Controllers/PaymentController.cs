using Domain.DTOs.PaymentDto;
using Domain.Filters;
using Domain.Responses;
using Infrastructure.Services.PaymentServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;
[ApiController]
[Route("[controller]")]
//[Authorize]
public class PaymentController(IPaymentService paymentService):ControllerBase
{
    [HttpGet("Get-Payments")]
    public async Task<PagedResponse<List<GetPaymentsDto>>> GetPaymentsAsync([FromQuery]PaymentFilter filter)
    {
        return await paymentService.GetPaymentsAsync(filter);
    }
    [HttpPost("Add-Payment")]
    public async Task<Response<string>> AddPaymentAsync(AddPaymentDto addPaymentDto)
    {
        return await paymentService.AddPaymentAsync(addPaymentDto);
    }
    [HttpPut("Update-Payment")]
    public async Task<Response<string>> UpdatePaymentAsync(UpdatePaymentDto updatePaymentDto)
    {
        return await paymentService.UpdatePaymentAsync(updatePaymentDto);
    }
    [HttpGet("Get-PaymentsById")]
    public async Task<Response<GetPaymentsDto>> GetPaymentsByIdAsync(int id)
    {
        return await paymentService.GetPaymentById(id);
    }
    [HttpDelete("Delete-Payment")]
    public async Task<Response<bool>> DeletePaymentAsync(int id)
    {
        return await paymentService.DeletePaymentAsync(id);
    }
}
