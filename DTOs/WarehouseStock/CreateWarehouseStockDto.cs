using System.ComponentModel.DataAnnotations;

public class CreateWarehouseStockDto
{

    [Required]
    public int IdArticle { get; set; }

    [Required]
    public int IdWarehouse { get; set; }

    [Required]
    public int CurrentStock { get; set; }

    [Required]
    public int MinimumStock { get; set; }

    [Required]
    public int MaximunStock { get; set; }

    [Required]
    public string Location { get; set; }
}