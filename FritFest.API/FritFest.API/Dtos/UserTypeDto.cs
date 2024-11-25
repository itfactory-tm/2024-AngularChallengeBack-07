namespace FritFest.API.Dtos;

public class UserTypeDto
{
    public Guid TypeId { get; set; }
    public string Naam { get; set; }
    
    public List<string> Users { get; set; }
}