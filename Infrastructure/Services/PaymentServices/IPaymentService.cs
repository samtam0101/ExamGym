using Domain.DTOs.PaymentDto;
using Domain.Filters;
using Domain.Responses;

namespace Infrastructure.Services.PaymentServices;

public interface IPaymentService
{
    Task<Response<string>> AddPaymentAsync(AddPaymentDto addPaymentDto);
    Task<Response<string>> UpdatePaymentAsync(UpdatePaymentDto updatePaymentDto);
    Task<PagedResponse<List<GetPaymentsDto>>> GetPaymentsAsync(PaymentFilter filter);
    Task<Response<bool>> DeletePaymentAsync(int id);
    Task<Response<GetPaymentsDto>> GetPaymentById(int id);
}
