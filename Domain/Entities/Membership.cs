namespace Domain.Entities;

public class Membership
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Type { get; set; }
    public decimal Price { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public User User { get; set; }
}