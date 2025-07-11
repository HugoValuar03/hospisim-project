﻿// <auto-generated />
using System;
using Hospisim.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Hospisim.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250611210136_TornarProntuarioKeyIdentity")]
    partial class TornarProntuarioKeyIdentity
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Hospisim.Models.AltaHospitalar", b =>
                {
                    b.Property<Guid>("InternacaoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CondicaoPaciente")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Data")
                        .HasColumnType("datetime2");

                    b.Property<string>("InstrucoesPosAlta")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("InternacaoId");

                    b.ToTable("AltasHospitalares");
                });

            modelBuilder.Entity("Hospisim.Models.Atendimento", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DataEHora")
                        .HasColumnType("datetime2");

                    b.Property<string>("Local")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("PacienteId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ProfissionalSaudeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("ProntuarioId")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int>("Tipo")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PacienteId");

                    b.HasIndex("ProfissionalSaudeId");

                    b.HasIndex("ProntuarioId");

                    b.ToTable("Atendimentos");
                });

            modelBuilder.Entity("Hospisim.Models.Especialidade", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Especialidades");
                });

            modelBuilder.Entity("Hospisim.Models.Exame", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AtendimentoId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DataRealizacao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataSolicitacao")
                        .HasColumnType("datetime2");

                    b.Property<string>("Resultado")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TipoExame")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AtendimentoId");

                    b.ToTable("Exames");
                });

            modelBuilder.Entity("Hospisim.Models.Internacao", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AtendimentoId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DataEntrada")
                        .HasColumnType("datetime2");

                    b.Property<string>("Leito")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MotivoInternacao")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ObservacoesClinicas")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("PacienteId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("PlanoSaudeAtualizado")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("PrevisaoAlta")
                        .HasColumnType("datetime2");

                    b.Property<string>("Quarto")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Setor")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StatusIternacao")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AtendimentoId")
                        .IsUnique();

                    b.HasIndex("PacienteId");

                    b.ToTable("Internacoes");
                });

            modelBuilder.Entity("Hospisim.Models.Paciente", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Cpf")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DataNascimento")
                        .HasColumnType("datetime2");

                    b.Property<string>("EnderecoCompleto")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EstadoCivil")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NomeCompleto")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("NumeroCartaoSus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PossuiPlanoSaude")
                        .HasColumnType("bit");

                    b.Property<string>("Sexo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telefone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TipoSanguineo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Pacientes");
                });

            modelBuilder.Entity("Hospisim.Models.Prescricao", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AtendimentoId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DataFim")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataInicio")
                        .HasColumnType("datetime2");

                    b.Property<string>("Dosagem")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Frequencia")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Medicamento")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Observacoes")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("ProfissionalSaudeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ReacoesAdversas")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StatusPrescricao")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ViaAdministracao")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AtendimentoId");

                    b.HasIndex("ProfissionalSaudeId");

                    b.ToTable("Prescricoes");
                });

            modelBuilder.Entity("Hospisim.Models.ProfissionalSaude", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<int>("CargaHorarioSemanal")
                        .HasColumnType("int");

                    b.Property<string>("Cpf")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DataAdmissao")
                        .HasColumnType("datetime2");

                    b.Property<string>("NomeCompleto")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RegistroConselho")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telefone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TipoRegistro")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Turno")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ProfissionaisSaude");
                });

            modelBuilder.Entity("Hospisim.Models.ProfissionalSaudeEspecialidade", b =>
                {
                    b.Property<Guid>("ProfissionalSaudeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("EspecialidadeId")
                        .HasColumnType("int");

                    b.HasKey("ProfissionalSaudeId", "EspecialidadeId");

                    b.HasIndex("EspecialidadeId");

                    b.ToTable("ProfissionalSaudeEspecialidades");
                });

            modelBuilder.Entity("Hospisim.Models.Prontuario", b =>
                {
                    b.Property<int>("NumeroProntuario")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("NumeroProntuario"));

                    b.Property<DateTime>("DataAbertura")
                        .HasColumnType("datetime2");

                    b.Property<string>("ObservacoesGerais")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("PacienteId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("NumeroProntuario");

                    b.HasIndex("PacienteId");

                    b.ToTable("Prontuarios");
                });

            modelBuilder.Entity("Hospisim.Models.AltaHospitalar", b =>
                {
                    b.HasOne("Hospisim.Models.Internacao", "Internacao")
                        .WithOne("AltaHospitalar")
                        .HasForeignKey("Hospisim.Models.AltaHospitalar", "InternacaoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Internacao");
                });

            modelBuilder.Entity("Hospisim.Models.Atendimento", b =>
                {
                    b.HasOne("Hospisim.Models.Paciente", "Paciente")
                        .WithMany("Atendimentos")
                        .HasForeignKey("PacienteId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Hospisim.Models.ProfissionalSaude", "ProfissionalSaude")
                        .WithMany("Atendimentos")
                        .HasForeignKey("ProfissionalSaudeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Hospisim.Models.Prontuario", "Prontuario")
                        .WithMany("Atendimentos")
                        .HasForeignKey("ProntuarioId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Paciente");

                    b.Navigation("ProfissionalSaude");

                    b.Navigation("Prontuario");
                });

            modelBuilder.Entity("Hospisim.Models.Exame", b =>
                {
                    b.HasOne("Hospisim.Models.Atendimento", "Atendimento")
                        .WithMany("Exames")
                        .HasForeignKey("AtendimentoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Atendimento");
                });

            modelBuilder.Entity("Hospisim.Models.Internacao", b =>
                {
                    b.HasOne("Hospisim.Models.Atendimento", "Atendimento")
                        .WithOne("Internacao")
                        .HasForeignKey("Hospisim.Models.Internacao", "AtendimentoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Hospisim.Models.Paciente", "Paciente")
                        .WithMany()
                        .HasForeignKey("PacienteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Atendimento");

                    b.Navigation("Paciente");
                });

            modelBuilder.Entity("Hospisim.Models.Prescricao", b =>
                {
                    b.HasOne("Hospisim.Models.Atendimento", "Atendimento")
                        .WithMany("Prescricoes")
                        .HasForeignKey("AtendimentoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Hospisim.Models.ProfissionalSaude", "ProfissionalSaude")
                        .WithMany()
                        .HasForeignKey("ProfissionalSaudeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Atendimento");

                    b.Navigation("ProfissionalSaude");
                });

            modelBuilder.Entity("Hospisim.Models.ProfissionalSaudeEspecialidade", b =>
                {
                    b.HasOne("Hospisim.Models.Especialidade", "Especialidade")
                        .WithMany("ProfissionalSaudeEspecialidade")
                        .HasForeignKey("EspecialidadeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Hospisim.Models.ProfissionalSaude", "ProfissionalSaude")
                        .WithMany("ProfissionalSaudeEspecialidades")
                        .HasForeignKey("ProfissionalSaudeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Especialidade");

                    b.Navigation("ProfissionalSaude");
                });

            modelBuilder.Entity("Hospisim.Models.Prontuario", b =>
                {
                    b.HasOne("Hospisim.Models.Paciente", "Paciente")
                        .WithMany("Prontuarios")
                        .HasForeignKey("PacienteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Paciente");
                });

            modelBuilder.Entity("Hospisim.Models.Atendimento", b =>
                {
                    b.Navigation("Exames");

                    b.Navigation("Internacao")
                        .IsRequired();

                    b.Navigation("Prescricoes");
                });

            modelBuilder.Entity("Hospisim.Models.Especialidade", b =>
                {
                    b.Navigation("ProfissionalSaudeEspecialidade");
                });

            modelBuilder.Entity("Hospisim.Models.Internacao", b =>
                {
                    b.Navigation("AltaHospitalar")
                        .IsRequired();
                });

            modelBuilder.Entity("Hospisim.Models.Paciente", b =>
                {
                    b.Navigation("Atendimentos");

                    b.Navigation("Prontuarios");
                });

            modelBuilder.Entity("Hospisim.Models.ProfissionalSaude", b =>
                {
                    b.Navigation("Atendimentos");

                    b.Navigation("ProfissionalSaudeEspecialidades");
                });

            modelBuilder.Entity("Hospisim.Models.Prontuario", b =>
                {
                    b.Navigation("Atendimentos");
                });
#pragma warning restore 612, 618
        }
    }
}
