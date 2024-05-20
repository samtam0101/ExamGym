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
using Microsoft.AspNetCore.Identity;
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

        services.AddIdentity<IdentityUser, IdentityRole>(config =>
            {
                config.Password.RequiredLength = 4;
                config.Password.RequireDigit = false; // must have at least one digit
                config.Password.RequireNonAlphanumeric = false; // must have at least one non-alphanumeric character
                config.Password.RequireUppercase = false; // must have at least one uppercase character
                config.Password.RequireLowercase = false;  // must have at least one lowercase character
            })
            //for registering usermanager and signinmanger
            .AddEntityFrameworkStores<DataContext>()
            .AddDefaultTokenProviders();
    }
}
