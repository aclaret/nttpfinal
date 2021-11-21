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
    }
}
