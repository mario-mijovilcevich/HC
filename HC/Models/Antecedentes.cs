using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HC.Models
{
    public class Antecedentes
    {
        public Antecedentes()
        {
            HistoriasClinicasList = new List<HistoriasClinicas>();
        }

        [Key]
        public int AntecedenteID { get; set; }

        public string Descripcion { get; set; }

        public virtual ICollection<HistoriasClinicas> HistoriasClinicasList { get; set; }
    }
}