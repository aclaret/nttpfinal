using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestoBart.Models
{
    public class Pedido
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdPedido { get; set; }
        public Cliente IdCliente { get; set; }
        public string Fecha { get; set; }
        public double Monto { get; set; }
        //public ICollection<Plato> Platos { get; set; }
        public List<PlatosXPedidos> PlatosXPedidos { get; set; }

        public Pedido() { }

        public Pedido(Cliente cliente, String Fecha, Double Monto)
        {
            this.IdCliente = cliente;
            this.Fecha = Fecha;
            this.Monto = Monto;
        }
    }
}
