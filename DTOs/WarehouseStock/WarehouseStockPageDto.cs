public class WarehouseStockPageDto
{
    public IEnumerable<WarehouseStockDto> WarehouseStock { get; set; } = new List<WarehouseStockDto>();
    public WarehouseStockPageResumeDto Resume { get; set; } = default!;

}

public class WarehouseStockDto
{
    public int IdArticle { get; set; }
    public int IdWarehouse { get; set; }
    public string ArticleName { get; set; }
    public string ArticleDescription { get; set; }
    public string WarehouseName { get; set; }
    public string Location { get; set; }
    public int CurrentStock { get; set; }
    public int MinStock { get; set; }
    public int MaxStock { get; set; }
    public string StatusText { get; set; }

}


public class WarehouseStockPageResumeDto
{
    public int TotalExistence { get; set; }
    public int CriticalStock { get; set; }
    public int LowStock { get; set; }
    public int OverStock { get; set; }
}