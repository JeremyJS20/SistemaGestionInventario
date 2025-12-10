using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Mono.TextTemplating;
using SistemaGestionInventario.Data;
using SistemaGestionInventario.DTOs;
using SistemaGestionInventario.Enums;
using SistemaGestionInventario.Models;
using SistemaGestionInventario.Pages.Shared.Types;

namespace SistemaGestionInventario.Pages.Transactions
{
    //[Authorize(Policy = "Permission.TRSCTN")]
    public class IndexModel : PageModel
    {
        [BindProperty]
        public TransactionDto TransactionDto { get; set; }
        private readonly SistemaGestionInventarioContext _context;

        public IndexModel(SistemaGestionInventarioContext context)
        {
            _context = context;
        }

        public List<Article> Articles { get; set; }
        public List<Warehouse> Warehouses { get; set; }
        public int Total { get; set; }
        public int Inbound { get; set; }
        public int Outbound { get; set; }
        public int Adjustment { get; set; }
        public int Pending { get; set; }

        public async Task OnGet()
        {
            ViewData["ActivePage"] = "Transactions";
            ViewData["PageRoutes"] = new List<RouteItem> {
                new RouteItem { Label = "Inventario > <strong>Transacciones</strong>" }
            };

            Articles = await _context.Articles.Where(a => a.State).AsNoTracking().ToListAsync();
            Warehouses = await _context.Warehouses.AsNoTracking().ToListAsync();

            var transactions = await _context.Transactions.ToListAsync();
            Total = transactions.Count();
            Inbound = transactions.Where(t => t.Type == (int)TransactionEnum.Inbound).Count();
            Outbound = transactions.Where(t => t.Type == (int)TransactionEnum.Outbound).Count();
            Adjustment = transactions.Where(t => t.Type == (int)TransactionEnum.Adjustment).Count();
            Pending = transactions.Where(t => t.State == (int)TransactionStatusEnum.pending).Count();
        }

        public async Task<IActionResult> OnPostEndpoint()
        {
            Dictionary<string, string> errors = new Dictionary<string, string>();
            ModelState.Remove("TransactionDto.Article");
            ModelState.Remove("TransactionDto.Warehouse");
            if (!ModelState.IsValid)
            {
                errors = ModelState
                    .Where(kvp => kvp.Value.Errors.Count > 0)
                    .ToDictionary(
                        kvp => kvp.Key,
                        kvp => kvp.Value.Errors.First().ErrorMessage
                    );
            }

            if (errors.Any()) return new JsonResult(new { success = false, errors });

            if (TransactionDto.Id == 0)
            {
                await _context.Transactions.AddAsync(new Transaction
                {
                    Type = TransactionDto.Type,
                    ArticleId = TransactionDto.ArticleId,
                    WarehouseId = TransactionDto.WarehouseId,
                    Amount = TransactionDto.Amount,
                    UnitPrice = TransactionDto.UnitPrice,
                    AdjustmentAmount = TransactionDto.AdjustmentAmount,
                    Motive = TransactionDto.Motive,
                    Reference = TransactionDto.Reference,
                    Note = TransactionDto.Note,
                    State = 0,
                    Date = DateTime.Now,
                });
            }

            await _context.SaveChangesAsync();

            return new JsonResult(new { success = true });
        }

        public async Task<IActionResult> OnGetTransactionTableAsync(string search, int type, int warehouseId, int state)
        {
            var transactionsQuery = _context.Transactions.AsQueryable();

            if (type != -1)
            {
                transactionsQuery = transactionsQuery.Where(t => t.Type == type);
            }

            if (warehouseId != -1)
            {
                transactionsQuery = transactionsQuery.Where(t => t.WarehouseId == warehouseId);
            }

            if (state != -1)
            {
                transactionsQuery = transactionsQuery.Where(t => t.State == state);
            }

            var transactions = await transactionsQuery.Select(t => new TransactionDto
            {
                Id = t.Id,
                Type = t.Type,
                ArticleId = t.ArticleId,
                WarehouseId = t.WarehouseId,
                Amount = t.Amount,
                UnitPrice = t.UnitPrice,
                AdjustmentAmount = t.AdjustmentAmount,
                Motive = t.Motive,
                Reference = t.Reference,
                Note = t.Note,
                State = t.State,
                Date = t.Date,
            }).ToListAsync();

            for (int i = 0; i < transactions.Count; i++)
            {
                transactions[i].Article = await _context.Articles.FirstOrDefaultAsync(a => a.Id == transactions[i].ArticleId);
                transactions[i].Warehouse = await _context.Warehouses.FirstOrDefaultAsync(a => a.Id == transactions[i].WarehouseId);
            }

            if (!string.IsNullOrEmpty(search))
            {
                transactions = transactions.Where(t => t.Article.Name.Contains(search) || t.Warehouse.Name.Contains(search) || (t.Motive?.Contains(search) ?? false) || (t.Reference?.Contains(search) ?? false)).ToList();
            }

            return new JsonResult(transactions);
        }

        public async Task<IActionResult> OnGetTransactionAsync(int id)
        {
            var transaction = await _context.Transactions.Select(t => new TransactionDto
            {
                Id = t.Id,
                Type = t.Type,
                ArticleId = t.ArticleId,
                WarehouseId = t.WarehouseId,
                Amount = t.Amount,
                UnitPrice = t.UnitPrice,
                AdjustmentAmount = t.AdjustmentAmount,
                Motive = t.Motive,
                Reference = t.Reference,
                Note = t.Note,
                State = t.State,
                Date = t.Date,
            }).FirstOrDefaultAsync(t => t.Id == id);
            
            transaction.Article = await _context.Articles.FirstOrDefaultAsync(a => a.Id == transaction.ArticleId);
            transaction.Warehouse = await _context.Warehouses.FirstOrDefaultAsync(a => a.Id == transaction.WarehouseId);

            return new JsonResult(transaction);
        }

        public async Task<IActionResult> OnPostTransactionStateAsync(int id, int state)
        {
            var transaction = await _context.Transactions.FirstOrDefaultAsync(t => t.Id == id);
            transaction.State = state;

            await _context.SaveChangesAsync();
            return RedirectToPage();
        }
    }
}
