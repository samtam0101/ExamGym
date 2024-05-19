using AutoMapper;
using Domain.DTOs.ClassScheduleDto;
using Domain.DTOs.MembershipDto;
using Domain.DTOs.PaymentDto;
using Domain.DTOs.TrainerDto;
using Domain.DTOs.UserDto;
using Domain.DTOs.WorkoutDto;
using Domain.Entities;

namespace Infrastructure.AutoMapper;

public class MapperProfile:Profile
{
    public MapperProfile()
    {
        CreateMap<Payment, AddPaymentDto>().ReverseMap();
        CreateMap<Payment, UpdatePaymentDto>().ReverseMap();
        CreateMap<Payment, GetPaymentsDto>().ReverseMap();

        CreateMap<User, AddUserDto>().ReverseMap();
        CreateMap<User, UpdateUserDto>().ReverseMap();
        CreateMap<User, GetUsersDto>().ReverseMap();

        CreateMap<Workout, AddWorkoutDto>().ReverseMap();
        CreateMap<Workout, UpdateWorkoutDto>().ReverseMap();
        CreateMap<Workout, GetWorkoutsDto>().ReverseMap();

        CreateMap<Trainer, AddTrainerDto>().ReverseMap();
        CreateMap<Trainer, UpdateTrainerDto>().ReverseMap();
        CreateMap<Trainer, GetTrainersDto>().ReverseMap();

        CreateMap<Membership, AddMembershipDto>().ReverseMap();
        CreateMap<Membership, UpdateMembershipDto>().ReverseMap();
        CreateMap<Membership, GetMembershipsDto>().ReverseMap();

        CreateMap<ClassSchedule, AddClassScheduleDto>().ReverseMap();
        CreateMap<ClassSchedule, UpdateClassScheduleDto>().ReverseMap();
        CreateMap<ClassSchedule, GetClassSchedulesDto>().ReverseMap();
    }
}
