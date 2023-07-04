using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class MiTiendaContext : DbContext
{
    public MiTiendaContext(DbContextOptions<MiTiendaContext> options) : base(options)
    {
    }
    public DbSet<Producto> Productos { get; set; }
    
}
