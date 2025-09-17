using System;
using System.Collections.Generic;

namespace SistemaGestionInventario.Models;

public partial class OrganizationUserRole
{
    public int IdUser { get; set; }

    public int IdOrganization { get; set; }

    public int IdRole { get; set; }

    public virtual Organization IdOrganizationNavigation { get; set; } = null!;

    public virtual Role IdRoleNavigation { get; set; } = null!;

    public virtual User IdUserNavigation { get; set; } = null!;
}
