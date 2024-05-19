namespace Domain.Entities;

public class Trainer
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Specialization { get; set; }
    public string? Photo { get; set; }
    public List<ClassSchedule> ClassSchedules { get; set; }
}