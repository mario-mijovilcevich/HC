using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HC.Models
{
    public class ObrasSociales
    {
        public ObrasSociales()
        {
            PacientesList = new List<Pacientes>();
        }

        [Key]
        public int ObraSocialID { get; set; }

        [Display(Name = "Obra Social")]
        public string Nombre { get; set; }

        public virtual ICollection<Pacientes> PacientesList { get; set; }
    }
}