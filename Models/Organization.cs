using System;
using System.Collections.Generic;

namespace SistemaGestionInventario.Models;

public partial class Organization
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Status { get; set; } = null!;

    public DateTime CreateAt { get; set; }

    public virtual ICollection<Role> Roles { get; set; } = new List<Role>();

    public ICollection<OrganizationUser> OrganizationUsers { get; set; } = new List<OrganizationUser>();

    public ICollection<OrganizationUserRole> OrganizationUserRole { get; set; } = new List<OrganizationUserRole>();

    public ICollection<OrganizationWarehouse> OrganizationWarehouses { get; set; } = new List<OrganizationWarehouse>();

    public ICollection<OrganizationCategory> OrganizationCategories { get; set; } = new List<OrganizationCategory>();

    public ICollection<OrganizationArticle> OrganizationArticles { get; set; } = new List<OrganizationArticle>();

}
