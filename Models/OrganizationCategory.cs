using System;
using System.Collections.Generic;

namespace SistemaGestionInventario.Models;

public partial class OrganizationCategory
{
    public int IdOrganization { get; set; }

    public int IdCategory { get; set; }

    public virtual Organization Organization { get; set; } = null!;

    public virtual Category Category { get; set; } = null!;

}
