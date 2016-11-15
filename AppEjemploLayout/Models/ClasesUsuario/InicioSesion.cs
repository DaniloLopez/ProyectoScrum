using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AppEjemploLayout.Models.ClasesUsuario
{
    public class InicioSesion
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Por favor ingrese su correo electronico")]
        [Display(Name = "Correo electronico*:")]
        public string correoSesion { get; set; }

        [Required(ErrorMessage = "Por favor ingrese su contraseña, debe tener almenos 8 caracteres")]
        [StringLength(18, ErrorMessage = "La contraseña debe tener al menos {2} caracteres", MinimumLength = 8)]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña*:")]
        public string contraseñaUsuario { get; set; }
    }
}