public class InventoryTypesPageDto
{
    public IEnumerable<InventoryTypesDto> Categories { get; set; } = new List<InventoryTypesDto>();
    public InventoryTypesPageDtoResumeDto Resume { get; set; } = default!;

}

public class InventoryTypesDto
{
    public int Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int TotalArticles { get; set; }
    public string Level { get; set; }
    public string LevelText { get; set; }
    public string ParentCategory { get; set; }
    public string Status { get; set; }
    public string StatusText { get; set; }

}


public class InventoryTypesPageDtoResumeDto
{
    public int TotalCategories { get; set; }
    public int TotalSubcategories { get; set; }
    public int TotalTypes { get; set; }
    public int TotaRelatedArticles { get; set; }
}