using System;
using System.Collections.Generic;

namespace SistemaGestionInventario.Models;

public partial class OrganizationWarehouse
{
    public int IdOrganization { get; set; }

    public int IdWarehouse { get; set; }
    public virtual Warehouse Warehouse { get; set; } = null!;

    public virtual Organization Organization { get; set; } = null!;
}
