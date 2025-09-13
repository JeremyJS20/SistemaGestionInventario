using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SistemaGestionInventario.Data;
using SistemaGestionInventario.Models;

namespace SistemaGestionInventario.Pages.EFConfig
{
    public class IndexModel : PageModel
    {
        private readonly SistemaGestionInventario.Data.SistemaGestionInventarioContext _context;

        public IndexModel(SistemaGestionInventario.Data.SistemaGestionInventarioContext context)
        {
            _context = context;
        }

        public IList<ConfigEntityFramework> ConfigEntityFramework { get;set; } = default!;

        public async Task OnGetAsync()
        {
            ConfigEntityFramework = await _context.ConfigEntityFramework.ToListAsync();
        }
    }
}
