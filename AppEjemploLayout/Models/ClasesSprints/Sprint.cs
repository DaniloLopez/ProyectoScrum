using AppEjemploLayout.Models.ClasesProyecto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AppEjemploLayout.Models.ClasesSprints
{
    public class Sprint
    {
        public int SprintId { get; set; }

        public int duracion { get; set; }

        public string estado { get; set; }

        public int ProyectoId { get; set; }

        [ForeignKey("ProyectoId")]
        public Proyecto proyecto { get; set; }

        public virtual List<ClasesHistoriaUsuario.HistoriaUsuario> historiasUsuario { get; set; }
    }
}