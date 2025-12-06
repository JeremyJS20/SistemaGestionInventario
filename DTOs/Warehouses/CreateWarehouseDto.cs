using System.ComponentModel.DataAnnotations;

public class CreateWarehouseDto
{

    [Required]
    public string Code { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string Description { get; set; }

    [Required]
    public string Address { get; set; }

    [Required]
    public string City { get; set; }

    [Required]
    public int Capacity { get; set; }

    [Required]
    public int Stock { get; set; }

    [Required]
    public string ResponsibleName { get; set; }

    [Required]
    public string Status { get; set; }
}