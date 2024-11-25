using System.ComponentModel.DataAnnotations;

namespace FritFest.API.Entities;

public class UserType
{
    [Key]
    public Guid TypeId { get; set; }
    public string Naam { get; set; }
    
    public ICollection<User> Users { get; set; }
}