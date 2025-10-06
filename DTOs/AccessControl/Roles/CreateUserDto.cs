using System.ComponentModel.DataAnnotations;

public class CreateUserDto
{
    [Required]
    public string Username { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string LastName { get; set; }

    [Required, EmailAddress]
    public string Email { get; set; }

    [Required]
    public string Status { get; set; }

    [Required]
    public int RoleId { get; set; }
}