using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HC.Models
{
    public class Consultas
    {
        [Key]
        public int ConsultaID { get; set; }

        public int HistoriaClinicaID { get; set; }

        public virtual HistoriasClinicas HistoriasClinicas { get; set; }

        [Display(Name = "Motivo de la Consulta")]
        public string MotivoConsulta { get; set; }

        [Display(Name = "Estado Físico")]
        public string EstadoFisico { get; set; }

        public string Indicaciones { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Fecha { get; set; }
    }
}