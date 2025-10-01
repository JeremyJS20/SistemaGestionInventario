using System;
using System.Collections.Generic;

namespace SistemaGestionInventario.Models;

public partial class Permission
{
    public int Id { get; set; }

    public string Key { get; set; } = null!;

    public string Category { get; set; } = null!;

    public string Name { get; set; } = null!;
}
