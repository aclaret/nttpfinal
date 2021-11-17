using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestoBart.Models
{
    public class Cliente : Usuario
    {
        [Required(ErrorMessage = "Ingrese un telefono válido")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Ingrese un telefono válido sin 0 ni 15")]
        [Display(Name = "Telefono")]
        public int Telefono { get; set; }

        [Required(ErrorMessage = "Ingrese un email válido")]
        [EmailAddress(ErrorMessage = "Ingrese un email válido")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Ingrese una dirección válida"), MinLength(3), MaxLength(20)]
        [Display(Name = "Dirección")]
        public string Direccion { get; set; }

        [Display(Name = "Piso")]
        public int Piso { get; set; }

        [Display(Name = "Departamento")]
        public char Departamento { get; set; }
    }
}
