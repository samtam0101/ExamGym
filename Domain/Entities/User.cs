namespace Domain.Entities;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public DateTime RegistrationDate { get; set; }
    public string Role { get; set; }
    public List<Membership> Memberships { get; set; }
    public List<Payment> Payments { get; set; }
}