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
        public DbSet<Administrador> Administradores { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Plato> Platos { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
    }
}
