using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HC.Models
{
    public class Pacientes
    {
        public Pacientes() {}

        [Key]
        public int PacienteID { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Messages), ErrorMessageResourceName = "PropertyValueRequired")]
        [Display(Name = "Tipo de Documento")]
        public int TipoDocumentoID { get; set; }

        public virtual TiposDocumentos TiposDocumentos { get; set; }
        
        public virtual HistoriasClinicas HistoriasClinicas { get; set; }
        
        [Required(ErrorMessageResourceType = typeof(Resources.Messages), ErrorMessageResourceName = "PropertyValueRequired")]
        [Display(Name = "Número de Documento")]
        public int NumeroDocumento { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Messages), ErrorMessageResourceName = "PropertyValueRequired")]
        public string Nombre { get; set; }
       
        [Required(ErrorMessageResourceType = typeof(Resources.Messages), ErrorMessageResourceName = "PropertyValueRequired")]
        public string Apellido { get; set; }

        [Display(Name = "Teléfono")]
        public string Telefono { get; set; }

        public string Email { get; set; }

        [Display(Name = "Dirección")]
        public string Direccion { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Messages), ErrorMessageResourceName = "PropertyValueRequired")]
        [Display(Name = "Fecha de Nacimiento")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime FechaNacimiento { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Messages), ErrorMessageResourceName = "PropertyValueRequired")]
        [Display(Name = "Obra Social")]
        public int ObraSocialID { get; set; }

        public virtual ObrasSociales ObrasSociales { get; set; }
    }
}