namespace Domain.DTOs.MembershipDto;

public class UpdateMembershipDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Type { get; set; }
    public decimal Price { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}
