using System.ComponentModel.DataAnnotations;

public class CreateInventoryTypeDto 
{

    [Required]
    public string Code { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string Description { get; set; }

    [Required]
    public string Level { get; set; }

    public int? ParentId { get; set; }

    [Required]
    public string Status { get; set; }
}