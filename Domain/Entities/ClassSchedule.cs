namespace Domain.Entities;

public class ClassSchedule
{
    public int Id { get; set; }
    public int WorkoutId { get; set; }
    public int TrainerId { get; set; }
    public DateTime DateTime { get; set; }
    public int Duration { get; set; }
    public string Location { get; set; }
    public Workout Workout { get; set; }
    public Trainer Trainer { get; set; }
}