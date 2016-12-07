using AppEjemploLayout.Models.ClasesHistoriaUsuario;
using AppEjemploLayout.Models.ClasesProyecto;
using AppEjemploLayout.Models.Proyecto_Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppEjemploLayout.Models.ViewModel
{
    public class InformacionDeProyecto
    {
        public Proyecto proyecto { get; set; }

        public List<ProyectoUsuarioRelacion> equipo { get; set; }

        public List<HistoriaUsuario> productBacklog { get; set; }

    }
}