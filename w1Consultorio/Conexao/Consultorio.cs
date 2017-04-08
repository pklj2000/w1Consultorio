using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data.Entity;
using System.Configuration;
using w1Consultorio.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace w1Consultorio
{
    public class Consultorio: DbContext
    {
        public Consultorio()
            : base("Consultorio")
        {
        }

        public DbSet<Models.Empresa> Empresas { get; set; }
        public DbSet<Departamento> Departamentos { get; set; }
        public DbSet<TipoExame> TiposExame { get; set; }
        public DbSet<EstadoCivil> EstadosCivis { get; set; }
        public DbSet<Risco> Riscos { get; set; }
        public DbSet<Periodicidade> Periodicidades { get; set; }
        public DbSet<Exame> Exames { get; set; }
        public DbSet<Cargo> Cargos { get; set; }
        public DbSet<Funcionario> Funcionarios { get; set; }
        public DbSet<Profissional> Profissionais { get; set; }
        public DbSet<PerguntaGrupo> PerguntaGrupos { get; set; }
        public DbSet<Pergunta> Pergunta { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Sistema> Sistema { get; set; }
        public DbSet<Modulo> Modulo { get; set; }
        public DbSet<Transacao> Transacao { get; set; }
        public DbSet<Perfil> Perfil { get; set; }
        public DbSet<Atendimento> Atendimento { get; set; }
        public DbSet<Prontuario> Prontuario { get; set; }
        public DbSet<Resposta> Resposta { get; set; }
 
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>()
                .HasMany(e => e.Perfil).WithMany(f => f.Usuario)
                .Map(t => t.MapLeftKey("CodUsuario")
                    .MapRightKey("CodPerfil")
                    .ToTable("UsuarioPerfil"));
            
            modelBuilder.Entity<Perfil>()
                .HasMany(e => e.Transacao).WithMany(f => f.Perfil)
                .Map(t => t.MapLeftKey("CodPerfil")
                    .MapRightKey("CodTransacao")
                    .ToTable("PerfilTransacao"));

            modelBuilder.Entity<Exame>()
                .HasMany(e => e.Periodicidade).WithMany(f => f.Exames)
                .Map(t => t.MapLeftKey("codExame")
                    .MapRightKey("codPeriodicidade")
                    .ToTable("ExamePeriodicidade"));

            modelBuilder.Entity<Cargo>()
                .HasMany(e => e.Exames).WithMany(f => f.Cargos)
                .Map(t => t.MapLeftKey("codCargo")
                    .MapRightKey("codExame")
                    .ToTable("CargoExame"));

            modelBuilder.Entity<Cargo>()
                .HasMany(e => e.Riscos).WithMany(f => f.Cargos)
                .Map(t => t.MapLeftKey("codCargo")
                    .MapRightKey("codRisco")
                    .ToTable("CargoRisco"));

            modelBuilder.Entity<Cargo>()
                .HasRequired(m => m.Departamento)
                .WithMany()
                .HasForeignKey(c => c.codDepartamento);

            modelBuilder.Entity<Cargo>()
                .HasRequired(m => m.Periodicidade)
                .WithMany()
                .HasForeignKey(c => c.codPeriodicidade);

            modelBuilder.Entity<Funcionario>()
                .HasRequired(f => f.Cargo)
                .WithMany()
                .HasForeignKey(c => c.CodCargo)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Funcionario>()
                .HasRequired(f => f.Periodicidade)
                .WithMany()
                .HasForeignKey(c => c.CodPeriodicidade)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Transacao>()
                .HasRequired(f => f.Modulo)
                .WithMany()
                .HasForeignKey(c => c.CodModulo)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Prontuario>()
                .HasRequired(f => f.Empresa)
                .WithMany()
                .HasForeignKey(c => c.CodEmpresa)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Prontuario>()
                .HasRequired(f => f.Departamento)
                .WithMany()
                .HasForeignKey(c => c.CodDepartamento)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Prontuario>()
                .HasRequired(f => f.Cargo)
                .WithMany()
                .HasForeignKey(c => c.CodCargo)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Prontuario>()
                .HasRequired(f => f.Funcionario)
                .WithMany()
                .HasForeignKey(c => c.CodFuncionario)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Prontuario>()
                .HasRequired(f => f.Profissional)
                .WithMany()
                .HasForeignKey(c => c.codProfissional)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Resposta>()
                .HasRequired(f => f.Prontuario)
                .WithMany()
                .HasForeignKey(c => c.CodProntuario)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Resposta>()
                .HasRequired(f => f.Pergunta)
                .WithMany()
                .HasForeignKey(c => c.CodPergunta)
                .WillCascadeOnDelete(false);

        }
    }
}