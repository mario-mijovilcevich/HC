using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HC.Models
{
    public class PEGs
    {
        [Key]
        public int PEGID { get; set; }

        public int HistoriaClinicaID { get; set; }

        public virtual HistoriasClinicas HistoriasClinicas { get; set; }

        public DateTime Fecha { get; set; }

        public string Basal { get; set; }

        public string MaxEsf { get; set; }

        public string CfMax { get; set; }

        public string CfUtil { get; set; }

        public string Conclusion { get; set; }
    }
}