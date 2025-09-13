using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemaGestionInventario.Data;
using SistemaGestionInventario.Models;

namespace SistemaGestionInventario.Pages.EFConfig
{
    public class EditModel : PageModel
    {
        private readonly SistemaGestionInventario.Data.SistemaGestionInventarioContext _context;

        public EditModel(SistemaGestionInventario.Data.SistemaGestionInventarioContext context)
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

            var configentityframework =  await _context.ConfigEntityFramework.FirstOrDefaultAsync(m => m.id == id);
            if (configentityframework == null)
            {
                return NotFound();
            }
            ConfigEntityFramework = configentityframework;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(ConfigEntityFramework).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ConfigEntityFrameworkExists(ConfigEntityFramework.id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ConfigEntityFrameworkExists(int id)
        {
            return _context.ConfigEntityFramework.Any(e => e.id == id);
        }
    }
}
