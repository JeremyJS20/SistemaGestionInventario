using System;
using System.Collections.Generic;

namespace SistemaGestionInventario.Models;

public partial class Category
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Code { get; set; }

    public string? Description { get; set; }

    public string Level { get; set; } = null!;

    public int? ParentId { get; set; }

    public string Status { get; set; } = null!;

    public ICollection<Article> Articles { get; set; } = new List<Article>();
}
