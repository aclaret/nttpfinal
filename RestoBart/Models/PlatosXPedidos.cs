using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestoBart.Models
{
    public class PlatosXPedidos
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdPlatoXPedido { get; set; }
        public int IdPedido { get; set; }
        public Pedido Pedido { get; set; }
        public int IdPlato { get; set; }
        public Plato Plato { get; set; }
        public int Cantidad { get; set; }
        public PlatosXPedidos() { }

        public PlatosXPedidos(int IdPedido, Plato Plato, int Cantidad)
        {
            this.IdPedido = IdPedido;
            this.Plato = Plato;
            this.Cantidad = Cantidad;
        }

        //public PlatosXPedidos(Pedido Pedido, Plato Plato, int Cantidad)
        //{
        //    this.Pedido = Pedido;
        //    this.Plato = Plato;
        //    this.Cantidad = Cantidad;
        //}
    }
}
