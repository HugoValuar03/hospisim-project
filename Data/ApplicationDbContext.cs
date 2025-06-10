using Microsoft.EntityFrameworkCore;
using Hospisim.Models;

namespace Hospisim.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Prontuario> Prontuarios { get; set; }
        public DbSet<Atendimento> Atendimentos { get; set; }
        public DbSet<ProfissionalSaude> ProfissionaisSaude { get; set; }
        public DbSet<Especialidade> Especialidades { get; set; }
        public DbSet<ProfissionalSaudeEspecialidade> ProfissionalSaudeEspecialidades { get; set; }
        public DbSet<Exame> Exames { get; set; }
        public DbSet<Prescricao> Prescricoes { get; set; }
        public DbSet<Internacao> Internacoes { get; set; }
        public DbSet<AltaHospitalar> AltasHospitalares { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProfissionalSaudeEspecialidade>()
                .HasKey(pse => new { pse.ProfissionalSaudeId, pse.EspecialidadeId });

            modelBuilder.Entity<Atendimento>()
                .HasOne(a => a.Paciente) 
                .WithMany(p => p.Atendimentos) 
                .HasForeignKey(a => a.PacienteId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Atendimento>()
                .HasOne(a => a.Prontuario)    
                .WithMany(p => p.Atendimentos)
                .HasForeignKey(a => a.ProntuarioId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Atendimento>()
                .HasOne(a => a.ProfissionalSaude)
                .WithMany(ps => ps.Atendimentos)
                .HasForeignKey(a => a.ProfissionalSaudeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Internacao>()
                .HasOne(i => i.Atendimento)
                .WithOne(a => a.Internacao)
                .HasForeignKey<Internacao>(i => i.AtendimentoId);

            modelBuilder.Entity<AltaHospitalar>()
                .HasOne(ah => ah.Internacao)
                .WithOne(i => i.AltaHospitalar)
                .HasForeignKey<AltaHospitalar>(ah => ah.InternacaoId)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder); 
        }
    }
}