using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HC.Models
{
    public class TiposDocumentos
    {
        public TiposDocumentos()
        {
            PacientesList = new List<Pacientes>();
        }

        [Key]
        public int TipoDocumentoID { get; set; }

        [Display(Name = "Tipo de Documento")]
        public string Descripcion { get; set; }

        public virtual ICollection<Pacientes> PacientesList { get; set; }
    }
}