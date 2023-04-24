using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HC.Models
{
    public class HistoriasClinicas
    {
        public HistoriasClinicas() 
        {
            FactoresRiesgoList = new List<FactoresRiesgo>();
            AntecedentesList = new List<Antecedentes>();
            MedicacionesList = new List<Medicaciones>();
            InstitucionesList = new List<Instituciones>();
            EstudiosList = new List<Estudios>();
            CCGsList = new List<CCGs>();
            ECOsList = new List<ECOs>();
            LaboratoriosList = new List<Laboratorios>();
            PEGsList = new List<PEGs>();
            ConsultasList = new List<Consultas>();
        }

        [Key]
        public int HistoriaClinicaID { get; set; }

        public int PacienteID { get; set; }

        [Display(Name = "Factores de Riesgo")]
        public virtual ICollection<FactoresRiesgo> FactoresRiesgoList { get; set; }

        [Display(Name = "Comentarios")]
        public string FactoresRiesgo { get; set; }

        [Display(Name = "Antecedentes")]
        public virtual ICollection<Antecedentes> AntecedentesList { get; set; }

        [Display(Name = "Comentarios")]
        public string Antecedentes { get; set; }

        [Display(Name = "Medicaciones")]
        public virtual ICollection<Medicaciones> MedicacionesList { get; set; }

        [Display(Name = "Comentarios")]
        public string Medicaciones { get; set; }

        [Display(Name = "Estudios Complementarios")]
        public string EstudiosComplementarios { get; set; }

        [Display(Name = "Instituciones")]
        public virtual ICollection<Instituciones> InstitucionesList { get; set; }

        public virtual ICollection<Estudios> EstudiosList { get; set; }

        public virtual ICollection<CCGs> CCGsList { get; set; }

        public virtual ICollection<ECOs> ECOsList { get; set; }

        public virtual ICollection<Laboratorios> LaboratoriosList { get; set; }

        public virtual ICollection<PEGs> PEGsList { get; set; }

        public virtual ICollection<Consultas> ConsultasList { get; set; }

        [Display(Name = "Fecha de Creación")]
        public DateTime? FechaCreacion { get; set; }

        [Display(Name = "Fecha de Actualización")]
        public DateTime? FechaActualizacion { get; set; }
    }
}