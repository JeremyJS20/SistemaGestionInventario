using System;
using System.Collections.Generic;

namespace SistemaGestionInventario.Models;

public partial class OrganizationWarehouse
{
    public int IdOrganization { get; set; }

    public int IdWarehouse { get; set; }

    public virtual Organization IdOrganizationNavigation { get; set; } = null!;

    public virtual Warehouse IdWarehouseNavigation { get; set; } = null!;
}
