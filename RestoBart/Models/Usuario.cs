﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestoBart.Models
{
    public abstract class Usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdUsuario { get; set; }

        [Required(ErrorMessage = "Ingrese un nombre válido"), MinLength(3, ErrorMessage = "El nombre debe tener mínimo 3 caracteres"), MaxLength(15, ErrorMessage = "El nombre debe tener como máximo 15 caracteres")]
        [Display(Name = "Nombre del usuario")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Ingrese un apellido válido"), MinLength(3, ErrorMessage = "El apellido debe tener mínimo 3 caracteres"), MaxLength(15, ErrorMessage = "El apellido debe tener como máximo 15 caracteres")]
        [Display(Name = "Apellido del usuario")]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "Ingrese un nombre de usuario entre 6 y 15 caracteres"), MinLength(6, ErrorMessage = "El nombre de usuario debe tener mínimo 6 caracteres"), MaxLength(15, ErrorMessage = "El nombre de usuario debe tener como máximo 15 caracteres")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Ingrese una password 6 y 15 (debe contener: 1 caracter especial, 1 número, 1 letra en mayúsculas, 1 letra en minúsculas)"), MinLength(6, ErrorMessage = "La contraseña debe tener mínimo 6 caracteres"), MaxLength(15, ErrorMessage = "La contraseña debe tener como máximo 15 caracteres")]
        [DataType(DataType.Password)]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[.,!@#$%^&*_=+-]).{6,15}$", ErrorMessage = "Ingrese una password 6 y 15 (debe contener: 1 caracter especial, 1 número, 1 letra en mayúsculas, 1 letra en minúsculas)")]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }
    }
}
