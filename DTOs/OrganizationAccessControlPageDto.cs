public class OrganizationAccessControlPageDto
{
    public IEnumerable<UserDto> Users { get; set; } = new List<UserDto>();
    public IEnumerable<RoleDto> Roles { get; set; } = new List<RoleDto>();
    public ResumeDto Resume { get; set; } = default!;

}

public class UserDto
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string Status { get; set; }
    public String LastAccess { get; set; }
    public IEnumerable<RoleDto> Roles { get; set; } = new List<RoleDto>();
}

public class RoleDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int TotalUsers { get; set; }
    public string Description { get; set; }
    public IEnumerable<PermissionDto> Permissions { get; set; } = new List<PermissionDto>();
}

public class PermissionDto
{
    public int Id { get; set; }
    public string Name { get; set; }
}

public class ResumeDto
{
    public int TotalUsers { get; set; }
    public int TotalActiveUsers { get; set; }
    public int TotalDefinedRoles { get; set; }
    public int TotalPermissions { get; set; }
}