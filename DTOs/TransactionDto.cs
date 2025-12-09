using SistemaGestionInventario.Models;
using System.ComponentModel.DataAnnotations;

namespace SistemaGestionInventario.DTOs
{
    public class TransactionDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El Tipo es requerido.")]
        public int? Type { get; set; }

        [Required(ErrorMessage = "El Articulo es requerido.")]
        public int? ArticleId { get; set; }

        [Required(ErrorMessage = "El Almacen es requerido.")]
        public int? WarehouseId { get; set; }

        [Required(ErrorMessage = "La Cantidad es requerida.")]
        public int? Amount { get; set; }

        public int? UnitPrice { get; set; }

        [Required(ErrorMessage = "La Cantidad es requerida.")]
        public int? AdjustmentAmount { get; set; }

        [Required(ErrorMessage = "El Motivo es requerido.")]
        public string? Motive { get; set; }

        public string? Reference { get; set; }

        public string? Note { get; set; }

        public int? State { get; set; }

        public DateTime? Date { get; set; }

        public Article Article { get; set; }

        public Warehouse Warehouse { get; set; }
    }
}
