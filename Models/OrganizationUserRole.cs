using System;
using System.Collections.Generic;

namespace SistemaGestionInventario.Models;

public partial class OrganizationUserRole
{
    public int IdUser { get; set; }

    public int IdOrganization { get; set; }

    public int IdRole { get; set; }

    public virtual Organization Organization { get; set; } = null!;

    public virtual Role Role { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
