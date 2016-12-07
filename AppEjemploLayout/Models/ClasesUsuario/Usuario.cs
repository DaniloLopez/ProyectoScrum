using AppEjemploLayout.Models.ClasesProyecto;
using AppEjemploLayout.Models.Proyecto_Usuario;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AppEjemploLayout.Models.ClasesUsuario
{
    public class Usuario
    {
        public int UsuarioId { get; set; }

        [EmailAddress]
        [Required]
        [Display(Name = "Correo Electronico*")]
        public string correoElectronicoUsuario { get; set; }

        [Required]
        [Display(Name = "Nombres*")]
        public string nombresUsuario { get; set; }

        [Required]
        [Display(Name = "Apellidos*")]
        public string apellidosUsuario { get; set; }

        [Display(Name = "Alias")]
        public string aliasUsuario { get; set; }

        [Required]        
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string contraseñaUsuario { get; set; }


        public virtual List<ProyectoUsuarioRelacion> Proyectos { get; set; }
    }
}