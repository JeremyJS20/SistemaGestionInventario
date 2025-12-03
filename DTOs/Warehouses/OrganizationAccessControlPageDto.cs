public class OrganizationWarehousesPageDto
{
    public IEnumerable<WarehouseDto> Warehouses { get; set; } = new List<WarehouseDto>();
    public OrganizationWarehousesPageResumeDto Resume { get; set; } = default!;

}

public class WarehouseDto
{
    public int Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string City { get; set; }
    public string Address { get; set; }
    public int Capacity { get; set; }
    public int Stock { get; set; }
    public string ResponsibleName { get; set; }
    public string Status { get; set; }
    public string StatusText { get; set; }

}


public class OrganizationWarehousesPageResumeDto
{
    public int TotalWarehouses { get; set; }
    public int TotalActiveWarehouses { get; set; }
    public int TotalWarehousesCapacity { get; set; }
    public int TotalWarehousesStock { get; set; }
}