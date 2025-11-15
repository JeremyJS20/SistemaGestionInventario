using Microsoft.AspNetCore.Mvc.RazorPages;


namespace SistemaGestionInventario.Pages.Reports.Articles
{
    //[Authorize(Policy = "Permission.AC")]
    public class IndexModel : PageModel
    {
        private readonly SistemaGestionInventario.Data.SistemaGestionInventarioContext _context;

        public IndexModel(SistemaGestionInventario.Data.SistemaGestionInventarioContext context)
        {
            _context = context;
        }

        public async Task OnGetAsync()
        {
        }
    }
}
