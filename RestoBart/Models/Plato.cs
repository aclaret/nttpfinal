using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestoBart.Models
{
    public class Plato
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdPlato { get; set; }

        [Display(Name = "Nombre del plato")]
        public string Nombre { get; set; }

        [Display(Name = "Precio")]
        public double Precio { get; set; }

        [Display(Name = "Categoría")]
        public Categoria Categoria { get; set; }
    }
}
