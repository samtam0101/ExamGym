using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Seed;
using Infrastructure.Services.AccountServices;
using Infrastructure.Services.ClassScheduleServices;
using Infrastructure.Services.FileService;
using Infrastructure.Services.MembershipServices;
using Infrastructure.Services.PaymentServices;
using Infrastructure.Services.TrainerServices;
using Infrastructure.Services.UserServices;
using Infrastructure.Services.WorkoutServices;
using Microsoft.EntityFrameworkCore;

namespace WebApi.ExtensionMethods.RegisterService;

public static class RegisterService
{
    public static void AddRegisterService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<DataContext>(configure =>
            configure.UseNpgsql(configuration.GetConnectionString("Connection")));

        services.AddScoped<Seeder>();
        services.AddScoped<IFileService, FileService>();
        services.AddScoped<IPaymentService, PaymentService>();
        services.AddScoped<IClassScheduleService, ClassScheduleService>();
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<ITrainerService, TrainerService>();
        services.AddScoped<IMembershipService, MembershipService>();
        services.AddScoped<IWorkoutService, WorkoutService>();
        services.AddScoped<IUserService, UserService>();
    }
}
