using System;
using System.Collections.Generic;

namespace SistemaGestionInventario.Models;

public partial class RolePermission
{
    public int IdRole { get; set; }

    public int IdPermission { get; set; }

    public virtual Permission Permission { get; set; } = null!;
    public virtual Role Role { get; set; } = null!;

}
