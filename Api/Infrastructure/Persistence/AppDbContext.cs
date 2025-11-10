using Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Api.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<ArchivoCliente> ArchivosCliente { get; set; }
        public DbSet<LogApi> LogsApi { get; set; }

    }
}
