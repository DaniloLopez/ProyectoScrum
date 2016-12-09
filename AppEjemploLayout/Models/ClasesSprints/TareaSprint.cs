using AppEjemploLayout.Models.ClasesUsuario;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AppEjemploLayout.Models.ClasesSprints
{
    public class TareaSprint
    {
        public int TareaSprintId { get; set; }

        [Required(ErrorMessage = "Por favor ingrese el asunto de la tarea.")]
        [Display(Name = "Asunto *: ")]
        public string asunto { get; set; }

        [Required(ErrorMessage = "Por favor ingrese la descripción de la tarea.")]
        [Display(Name = "Descripción* : ")]
        public string descripcion { get; set; }
        public string estado { get; set; }

        [Required(ErrorMessage = "Por favor ingrese la estimacion en horas de la tarea.")]
        [Display(Name = "Estimacion horas* : ")]
        public int estimacionHoras { get; set; }

        public int SprintId { get; set; }

        [ForeignKey("SprintId")]
        public Sprint sprint { get; set; }

        public int? UsuarioId { get; set; }

        [ForeignKey("UsuarioId")]
        public Usuario usuario { get; set; }



    }
}