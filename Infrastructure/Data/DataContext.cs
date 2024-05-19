using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class DataContext : IdentityDbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options){}
    public DbSet<ClassSchedule> ClassSchedules { get; set; }
    public DbSet<Membership> Memberships { get; set; }
    public DbSet<Trainer> Trainers { get; set; }
    public DbSet<Workout> Workouts { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Payment> Payments { get; set; }

}