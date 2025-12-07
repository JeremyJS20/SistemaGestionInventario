using System;
using System.Collections.Generic;

namespace SistemaGestionInventario.Models;

public partial class OrganizationArticle
{
    public int IdOrganization { get; set; }

    public int IdArticle { get; set; }

    public virtual Organization Organization { get; set; } = null!;

    public virtual Article Article { get; set; } = null!;
}
