using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HC.Models
{
    public class CCGs
    {
        [Key]
        public int CCGID { get; set; }

        public int HistoriaClinicaID { get; set; }

        public virtual HistoriasClinicas HistoriasClinicas { get; set; }

        public DateTime Fecha { get; set; }

        public string Tci { get; set; }

        public string Da { get; set; }

        public string Cx { get; set; }

        public string Cd { get; set; }

        public string Conclusion { get; set; }
    }
}