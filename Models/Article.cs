using System;
using System.Collections.Generic;

namespace SistemaGestionInventario.Models;

public partial class Article
{
    public int Id { get; set; }

    public string Code { get; set; } = null!;

    public int Category { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public double Price { get; set; }

    public int Stock { get; set; }

    public int MinimumStock { get; set; }

    public bool State { get; set; }
}
