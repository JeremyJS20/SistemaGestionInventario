using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SistemaGestionInventario.Enums;
using SistemaGestionInventario.Models;
using SistemaGestionInventario.Pages.Shared.Types;
using System.Security.Claims;

namespace SistemaGestionInventario.Pages.AccessControl
{
    [Authorize(Policy = "Permission.AC")]
    public class IndexModel : PageModel
    {
        private readonly SistemaGestionInventario.Data.SistemaGestionInventarioContext _context;

        public IndexModel(SistemaGestionInventario.Data.SistemaGestionInventarioContext context)
        {
            _context = context;
        }

        public OrganizationAccessControlPageDto OrganizationAccessControlPageDto { get; set; } = default!;

        public IList<UserStatusEnum> Statuses { get; set; } = UserStatusEnum.GetAll();

        public Dictionary<string, List<Permission>> SystemPermissions { get; set; } = default!;

        public async Task OnGetAsync()
        {
            this.OrganizationAccessControlPageDto = await _context.Organizations
                .Where(or => or.Id == int.Parse(User.FindFirstValue("SelectedOrganizationId")!))
                .Select(or => new OrganizationAccessControlPageDto
                {
                    Users = or.OrganizationUsers
                        .Where(or => or.User.Id != int.Parse(User.FindFirstValue("UserId")!))
                        .Select(orgUser => new UserDto
                        {
                            Id = orgUser.User.Id,
                            Username = orgUser.User.Username,
                            FullName = $"{orgUser.User.Name} {orgUser.User.LastName}",
                            Email = orgUser.User.Email,
                            Status = UserStatusEnum.FromCode(orgUser.User.Status).Description,
                            LastAccess = DateTime.Now.ToString("d/M/yyyy"),
                            Roles = orgUser.OrganizationUserRole
                                .Select(our => new RoleDto
                                {
                                    Id = our.Role.Id,
                                    Name = our.Role.Name,
                                    Description = our.Role.Description,
                                }).ToList()
                        }).ToList(),
                
                    Roles = or.Roles
                        .Select(r => new RoleDto
                        {
                            Id = r.Id,
                            Name = r.Name,
                            TotalUsers=r.OrganizationUserRole.Select(our => our.User).Count(),
                            Description = r.Description,
                            Permissions = r.RolePermissions
                                .Select(p => new PermissionDto
                                {
                                    Id = p.Permission.Id,
                                    Name = p.Permission.Name
                                }).ToList()
                        }).ToList(),
                    Resume = new ResumeDto
                    {
                        TotalUsers=or.OrganizationUsers.Where(u => u.User.Id != int.Parse(User.FindFirstValue("UserId")!)).Count(),
                        TotalActiveUsers=or.OrganizationUsers.Where(u => u.User.Status == UserStatusEnum.AC.Code && u.User.Id != int.Parse(User.FindFirstValue("UserId")!)).Count(),
                        TotalDefinedRoles=or.Roles.Count(),
                        TotalPermissions= _context.Permissions.Count()
                    }
                })
                .FirstOrDefaultAsync();

            this.SystemPermissions = await _context.Permissions
                .Select(p => p)
                .GroupBy(p => p.Category)
                .ToDictionaryAsync(g => g.Key, g => g.ToList());

            ViewData["ActivePage"] = "AccessControl";
            ViewData["PageRoutes"] = new List<RouteItem> {
                new RouteItem{Label="Control de Acceso"}
            };
        }
    }
}
