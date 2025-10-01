using System;
using System.Collections.Generic;

namespace SistemaGestionInventario.Models;

public partial class OrganizationUser
{
    public int IdUser { get; set; }

    public int IdOrganization { get; set; }

    public DateTime JoinedAt { get; set; }

    public virtual Organization Organization { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}