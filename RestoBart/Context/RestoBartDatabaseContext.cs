using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestoBart.Models;

namespace RestoBart.Context
{
    public class RestoBartDatabaseContext : DbContext
    {
        public
       RestoBartDatabaseContext(DbContextOptions<RestoBartDatabaseContext> options)
       : base(options)
        {
        }

        public DbSet<Cliente> Clientes { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<PlatosXPedidos>()
                .HasOne(b => b.Pedido)
                .WithMany(ba => ba.PlatosXPedidos)
                .HasForeignKey(bi => bi.IdPedido);

            modelBuilder.Entity<PlatosXPedidos>()
                .HasOne(b => b.Plato)
                .WithMany(ba => ba.PlatosXPedidos)
                .HasForeignKey(bi => bi.IdPlato);
        }

        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<Plato> Platos { get; set; }
        public DbSet<PlatosXPedidos> PlatosXPedidos { get; set; }
    }
}
