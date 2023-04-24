using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HC.Models
{
    public class ECOs
    {
        [Key]
        public int ECOID { get; set; }

        public int HistoriaClinicaID { get; set; }

        public virtual HistoriasClinicas HistoriasClinicas { get; set; }

        public DateTime Fecha { get; set; }

        public int Vi { get; set; }

        public int S { get; set; }

        public int PP { get; set; }

        public int Ao { get; set; }

        public int Ai { get; set; }

        public int Fey { get; set; }

        public string Conclusion { get; set; }
    }
}