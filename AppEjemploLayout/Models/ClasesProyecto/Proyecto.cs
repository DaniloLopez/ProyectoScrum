using AppEjemploLayout.Models.ClasesUsuario;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AppEjemploLayout.Models.ClasesProyecto
{
    [Table ("Productos")]
    public class Proyecto
    {
        public int ProyectoId { get; set; }

        [Required(ErrorMessage = "Por favor ingrese el nombre del proyecto.")]
        [Display(Name ="Nombre del proyecto * : ")]
        public string nombreProyecto { get; set; }

        
        [Display(Name = "Descripcion del proyecto : ")]
        public string descripcionProyecto { get; set; }

        [Display(Name = "Fecha de inicio de proyecto : ")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime fechaInicioProyecto { get; set; }

        [Display(Name = "Fecha de finalizacion del proyecto : ")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime fechaFinalizacionProyecto { get; set; }

        [Display(Name ="Estado del proyecto : ")]
        public string estadoProyecto { get; set; }


        [Display(Name = "Historias de usuario : ")]
        public virtual ICollection<ClasesHistoriaUsuario.HistoriaUsuario> historiasUsuario { get; set; }
        
    }
}