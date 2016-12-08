using AppEjemploLayout.Models.ClasesHistoriaUsuario;
using AppEjemploLayout.Models.ClasesSprints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppEjemploLayout.Models.ViewModel
{
    public class InformacionSprint
    {
        public Sprint sprint { get; set; }
        public List<HistoriaUsuario> historiasUsuario { get; set; }
        public List<TareaSprint> tareas { get; set; }
    }
}