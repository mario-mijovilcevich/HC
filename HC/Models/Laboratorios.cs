using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HC.Models
{
    public class Laboratorios
    {
        [Key]
        public int LaboratorioID { get; set; }

        public int HistoriaClinicaID { get; set; }

        public virtual HistoriasClinicas HistoriasClinicas { get; set; }

        public DateTime Fecha { get; set; }

        public decimal Hto { get; set; }

        public decimal Hb { get; set; }

        public int Gb { get; set; }

        public int Pq { get; set; }

        public int Na { get; set; }

        public decimal K { get; set; }

        public int Glucosa { get; set; }

        public int HbGlicosilada { get; set; }

        public int Col { get; set; }

        public int Hdl { get; set; }

        public int Tg { get; set; }

        public int LdlTotal { get; set; }

        public int Urea { get; set; }

        public int Creatinina { get; set; }

        public string Otros { get; set; }
    }
}