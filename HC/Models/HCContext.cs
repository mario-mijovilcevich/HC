using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace HC.Models
{
    public class HCContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public HCContext() : base("name=HCConnection")
        {
        }

        public System.Data.Entity.DbSet<HC.Models.TiposEstudios> TiposEstudios { get; set; }

        public System.Data.Entity.DbSet<HC.Models.TiposDocumentos> TiposDocumentos { get; set; }

        public System.Data.Entity.DbSet<HC.Models.ObrasSociales> ObrasSociales { get; set; }

        public System.Data.Entity.DbSet<HC.Models.FactoresRiesgo> FactoresRiesgo { get; set; }

        public System.Data.Entity.DbSet<HC.Models.Antecedentes> Antecedentes { get; set; }

        public System.Data.Entity.DbSet<HC.Models.Medicaciones> Medicaciones { get; set; }

        public System.Data.Entity.DbSet<HC.Models.Pacientes> Pacientes { get; set; }

        public System.Data.Entity.DbSet<HC.Models.HistoriasClinicas> HistoriasClinicas { get; set; }

        public System.Data.Entity.DbSet<HC.Models.Instituciones> Instituciones { get; set; }    

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pacientes>().HasRequired<TiposDocumentos>(t => t.TiposDocumentos)
                .WithMany(p => p.PacientesList).HasForeignKey(t => t.TipoDocumentoID);
            modelBuilder.Entity<Pacientes>().HasRequired<ObrasSociales>(o => o.ObrasSociales)
                .WithMany(p => p.PacientesList).HasForeignKey(o => o.ObraSocialID);

            modelBuilder.Entity<HistoriasClinicas>()
                .HasMany(f => f.FactoresRiesgoList)
                .WithMany(h => h.HistoriasClinicasList);
            modelBuilder.Entity<HistoriasClinicas>()
                .HasMany(a => a.AntecedentesList)
                .WithMany(h => h.HistoriasClinicasList);
            modelBuilder.Entity<HistoriasClinicas>()
                .HasMany(m => m.MedicacionesList)
                .WithMany(h => h.HistoriasClinicasList);
            modelBuilder.Entity<HistoriasClinicas>()
                .HasMany(i => i.InstitucionesList)
                .WithMany(h => h.HistoriasClinicasList);

            modelBuilder.Entity<Estudios>().HasRequired<HistoriasClinicas>(t => t.HistoriasClinicas)
                .WithMany(p => p.EstudiosList).HasForeignKey(t => t.HistoriaClinicaID);
            modelBuilder.Entity<Estudios>().HasRequired<TiposEstudios>(t => t.TiposEstudios)
                .WithMany(p => p.EstudiosList).HasForeignKey(t => t.TipoEstudioID);

            modelBuilder.Entity<CCGs>().HasRequired<HistoriasClinicas>(t => t.HistoriasClinicas)
                .WithMany(p => p.CCGsList).HasForeignKey(t => t.HistoriaClinicaID);

            modelBuilder.Entity<ECOs>().HasRequired<HistoriasClinicas>(t => t.HistoriasClinicas)
                .WithMany(p => p.ECOsList).HasForeignKey(t => t.HistoriaClinicaID);

            modelBuilder.Entity<Laboratorios>().HasRequired<HistoriasClinicas>(t => t.HistoriasClinicas)
                .WithMany(p => p.LaboratoriosList).HasForeignKey(t => t.HistoriaClinicaID);

            modelBuilder.Entity<PEGs>().HasRequired<HistoriasClinicas>(t => t.HistoriasClinicas)
                .WithMany(p => p.PEGsList).HasForeignKey(t => t.HistoriaClinicaID);

            base.OnModelCreating(modelBuilder);
        }

        public System.Data.Entity.DbSet<HC.Models.Estudios> Estudios { get; set; }

        public System.Data.Entity.DbSet<HC.Models.CCGs> CCGs { get; set; }

        public System.Data.Entity.DbSet<HC.Models.ECOs> ECOs { get; set; }

        public System.Data.Entity.DbSet<HC.Models.Laboratorios> Laboratorios { get; set; }

        public System.Data.Entity.DbSet<HC.Models.PEGs> PEGs { get; set; }

        public System.Data.Entity.DbSet<HC.Models.Consultas> Consultas { get; set; }

    }
}
