using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HC.Models
{
    public class Medicaciones
    {
        public Medicaciones()
        {
            HistoriasClinicasList = new List<HistoriasClinicas>();
        }

        [Key]
        public int MedicacionID { get; set; }

        public string Nombre { get; set; }

        public virtual ICollection<HistoriasClinicas> HistoriasClinicasList { get; set; }
    }
}