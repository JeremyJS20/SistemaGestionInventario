using System;
using System.Collections.Generic;

namespace SistemaGestionInventario.Models;

public partial class Transaction
{
    public int Id { get; set; }

    public int? Type { get; set; }

    public int? ArticleId { get; set; }

    public int? WarehouseId { get; set; }

    public int? Amount { get; set; }

    public int? UnitPrice { get; set; }

    public int? AdjustmentAmount { get; set; }

    public string? Motive { get; set; }

    public string? Reference { get; set; }

    public string? Note { get; set; }

    public int? State { get; set; }

    public DateTime? Date { get; set; }
}
