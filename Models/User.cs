using System;
using System.Collections.Generic;

namespace SistemaGestionInventario.Models;

public partial class User
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Status { get; set; } = null!;

    public ICollection<OrganizationUser> OrganizationUsers { get; set; } = new List<OrganizationUser>();
}
