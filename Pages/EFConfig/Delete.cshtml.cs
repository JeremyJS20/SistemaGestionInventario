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
    public class DeleteModel : PageModel
    {
        private readonly SistemaGestionInventario.Data.SistemaGestionInventarioContext _context;

        public DeleteModel(SistemaGestionInventario.Data.SistemaGestionInventarioContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ConfigEntityFramework ConfigEntityFramework { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var configentityframework = await _context.ConfigEntityFramework.FirstOrDefaultAsync(m => m.id == id);

            if (configentityframework is not null)
            {
                ConfigEntityFramework = configentityframework;

                return Page();
            }

            return NotFound();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var configentityframework = await _context.ConfigEntityFramework.FindAsync(id);
            if (configentityframework != null)
            {
                ConfigEntityFramework = configentityframework;
                _context.ConfigEntityFramework.Remove(ConfigEntityFramework);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
