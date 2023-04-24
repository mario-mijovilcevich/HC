using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HC.Models
{
    public class TiposEstudios
    {
        public TiposEstudios()
        {
            EstudiosList = new List<Estudios>();
        }

        [Key]
        public int TipoEstudioID { get; set; }

        public string Descripcion { get; set; }

        public virtual ICollection<Estudios> EstudiosList { get; set; }
    }
}