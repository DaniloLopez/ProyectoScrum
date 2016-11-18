using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AppEjemploLayout.Models.ClasesUsuario
{
    public class Registro
    {
        public int Id { get; set; }

        [StringLength(100, ErrorMessage = "Este campo debe tener maximo 50 caracteres"  )]
        [EmailAddress]
        [Required(ErrorMessage ="Por favor ingrese su correo electronico")]
        [Display(Name = "Correo Electronico*:")]
        public string correoElectronicoUsuario { get; set; }

        [StringLength(60, ErrorMessage = "Este campo debe tener maximo 60 caracteres")]
        [Required(ErrorMessage = "Por favor ingrese su/s nombre/s:")]
        [Display(Name = "Nombres*:")]
        public string nombresUsuario { get; set; }

        [StringLength(60, ErrorMessage = "Este campo debe tener maximo 60 caracteres")]
        [Required(ErrorMessage = "Por favor ingrese su/s apellido/s")]
        [Display(Name = "Apellidos*:")]
        public string apellidosUsuario { get; set; }

        [StringLength(30,ErrorMessage ="Este campo debe tener maximo 30 caracteres")]
        [Display(Name = "Alias:")]
        public string aliasUsuario { get; set; }


        [Required(ErrorMessage = "Por favor ingrese su contraseña, debe tener almenos 8 caracteres")]
        [StringLength(30, ErrorMessage = "La contraseña debe tener al menos {2} caracteres.", MinimumLength = 8)]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña*:")]
        public string contraseñaUsuario { get; set; }

        [Required(ErrorMessage = "Por favor repita su contraseña")]
        [Display(Name = "Repita su contraseña*:")]
        [DataType(DataType.Password)]
        [Compare("contraseñaUsuario", ErrorMessage = "Las contraseñas no coinciden, intente de nuevo")]
        public string ComparePass { get; set; }


    }
}