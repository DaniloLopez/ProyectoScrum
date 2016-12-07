using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using AppEjemploLayout.Models.ClasesUsuario;
using AppEjemploLayout.Models.ClasesProyecto;

namespace AppEjemploLayout.Models.Proyecto_Usuario
{
    public class ProyectoUsuarioRelacion
    {
        public int Id { get; set; }

        public int UsuarioID { get; set; }

        public Usuario usuario { get; set; }

        public int ProyectoId { get; set; }

        [ForeignKey("ProyectoId")]
        public Proyecto proyecto { get; set; }

        public string rolUsuario { get; set; }        
    }
}