using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Warehouse.Models;

namespace Warehouse.Data
{
    public class WarehouseContext : DbContext
    {
        public WarehouseContext (DbContextOptions<WarehouseContext> options)
            : base(options)
        {
        }

        public DbSet<Warehouse.Models.Marca> Marcas { get; set; } = default!;
        public DbSet<Warehouse.Models.Producto> Productos { get; set; } = default!;
        public DbSet<Warehouse.Models.Departamento> Departamentos { get; set; } = default!;


    }
}
