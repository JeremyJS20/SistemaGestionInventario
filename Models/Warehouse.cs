using System;
using System.Collections.Generic;

namespace SistemaGestionInventario.Models;

public partial class Warehouse
{
    public int Id { get; set; }

    public string Code { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string City { get; set; } = null!;

    public int Capacity { get; set; }

    public int Stock { get; set; }

    public string ResponsibleName { get; set; } = null!;

    public string Status { get; set; } = null!;

    public ICollection<WarehouseArticle> WarehouseArticles { get; set; } = new List<WarehouseArticle>();

}
