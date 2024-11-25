using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FritFest.API.Entities;

public class User
{
    [Key]
    public Guid UserId { get; set; }
    [Required]
    public string Naam { get; set; }
    [EmailAddress]
    public string Email { get; set; }
    [Phone]
    public string Phone { get; set; }
    
    [ForeignKey(nameof(UserType))]
    public Guid TypeId { get; set; }
    
    public UserType Type { get; set; }
}