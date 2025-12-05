using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SistemaGestionInventario.Enums;
using SistemaGestionInventario.Models;
using SistemaGestionInventario.Pages.Shared.Types;
using System.Security.Claims;

namespace SistemaGestionInventario.Pages.Reports
{
    //[Authorize(Policy = "Permission.AC")]
    public class IndexModel : PageModel
    {

        public IndexModel()
        {
        }

        public async Task OnGetAsync()
        {
        }
    }
}
