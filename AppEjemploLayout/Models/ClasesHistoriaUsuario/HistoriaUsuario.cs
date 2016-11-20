using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AppEjemploLayout.Models.ClasesHistoriaUsuario
{
    [Table("HistoriasUsuario")]
    public class HistoriaUsuario
    {        
        public int HistoriaUsuarioId { get; set; }

        [Required(ErrorMessage = "Por favor ingrese el nombre de la historia de usuario.")]
        [Display(Name = "Nombre *:")]
        public string nombre { get; set; }

        [Required(ErrorMessage = "Por favor ingrese el contexto de la historia de usuario.")]
        [Display(Name = "Contexto *:")]
        public string contexto { get; set; }

        [Required(ErrorMessage = "Por favor ingrese a descripción de la historia de usuario.")]
        [Display(Name = "Descripción : ")]
        public string descripcion { get; set; }

        [Display(Name = "Prioridad : ")]
        public int prioridad { get; set; }

        [Display(Name = "Esfuerzo : ")]
        public int ezfuerzo { get; set; }

    }
}