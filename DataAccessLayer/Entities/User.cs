using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Entities;

public class User
{
    public Guid Id { get; set; }

    [Required]
    [MaxLength(20)]
    public string Username { get; set; }

    [Required]
    [MaxLength(50)]
    public string Email { get; set; }

    [Required]
    public string PasswordHash { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime UpdatedAt { get; set; }


    public List<Task> Tasks { get; set; } = new();
}
