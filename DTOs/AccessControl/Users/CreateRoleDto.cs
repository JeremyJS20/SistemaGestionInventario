using System.ComponentModel.DataAnnotations;

public class CreateRoleDto
{

    [Required]
    public string Name { get; set; }

    [Required]
    public string Description { get; set; }

    //[Required]
    //public string IsAdminStr { get; set; } = "false";

    [Required]
    public List<int> SelectedPermissionIds { get; set; }
}