namespace Domain.Entities;

public class Workout
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public int Duration { get; set; }
    public string Intensity { get; set; }
    public List<ClassSchedule> ClassSchedules { get; set; }
}