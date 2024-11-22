namespace FritFest.API.Dtos;

public class UserDto
{
    public Guid UserId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public Guid TypeId { get; set; }
    public string TypeName {get; set;} //mapped from type.name
}