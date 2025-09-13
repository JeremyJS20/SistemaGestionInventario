using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SistemaGestionInventario.Models;

namespace SistemaGestionInventario.Data
{
    public class SistemaGestionInventarioContext : DbContext
    {
        public SistemaGestionInventarioContext (DbContextOptions<SistemaGestionInventarioContext> options)
            : base(options)
        {
        }

        public DbSet<SistemaGestionInventario.Models.ConfigEntityFramework> ConfigEntityFramework { get; set; } = default!;
    }
}
