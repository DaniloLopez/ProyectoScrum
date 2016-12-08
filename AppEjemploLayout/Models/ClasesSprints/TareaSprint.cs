using AppEjemploLayout.Models.ClasesUsuario;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AppEjemploLayout.Models.ClasesSprints
{
    public class TareaSprint
    {
        public int TareaSprintId { get; set; }
        public string asunto { get; set; }
        public string descripcion { get; set; }
        public string estado { get; set; }
        public int estimacionHoras { get; set; }

        public int SprintId { get; set; }

        [ForeignKey("SprintId")]
        public Sprint sprint { get; set; }

        public int? UsuarioId { get; set; }

        [ForeignKey("UsuarioId")]
        public Usuario usuario { get; set; }



    }
}