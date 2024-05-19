namespace Domain.DTOs.WorkoutDto;

public class UpdateWorkoutDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public int Duration { get; set; }
    public string Intensity { get; set; }
}
