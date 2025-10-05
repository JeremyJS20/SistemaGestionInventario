using System;
using System.Collections.Generic;

namespace SistemaGestionInventario.Models;

public partial class Role
{
    public int Id { get; set; }

    public int IdOrganization { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public bool IsAdmin { get; set; }

    public virtual Organization Organization { get; set; } = null!;

    public ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();

    public ICollection<OrganizationUserRole> OrganizationUserRole { get; set; } = new List<OrganizationUserRole>();

}
