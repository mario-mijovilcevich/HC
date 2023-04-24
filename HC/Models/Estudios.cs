using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HC.Models
{
    public class Estudios
    {
        public Estudios() { }

        [Key]
        public int EstudioID { get; set; }

        public int HistoriaClinicaID { get; set; }

        public virtual HistoriasClinicas HistoriasClinicas { get; set; }

        [Display(Name = "Tipo de Estudio")]
        public int TipoEstudioID { get; set; }

        public virtual TiposEstudios TiposEstudios { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Fecha { get; set; }

        public string Conclusion { get; set; }
    }
}