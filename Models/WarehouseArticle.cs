using System;
using System.Collections.Generic;

namespace SistemaGestionInventario.Models;

public partial class WarehouseArticle
{
    public int IdWarehouse { get; set; }

    public int IdArticle { get; set; }

    public int CurrentStock { get; set; }

    public int MinimunStock { get; set; }

    public int MaximunStock { get; set; }

    public string Location { get; set; } = null!;

    public virtual Warehouse Warehouse { get; set; } = null!;

    public virtual Article Article { get; set; } = null!;
}
