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

        public ICollection<Plato> Platos { get; set; }

        //[ForeignKey]
        public int IdUsuario { get; set; }
        public Cliente cliente;
        public string Fecha { get; set; }
        public double Monto { get; set; }

    }
}
