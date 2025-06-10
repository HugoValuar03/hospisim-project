using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema; // Certifique-se de que está aqui para ForeignKey
using System.ComponentModel.DataAnnotations;
using System.ComponentModel; // Certifique-se de que está aqui para Key

namespace Hospisim.Models
{
    public class Internacao
    {
        public Guid Id { get; set; }

        public Guid PacienteId { get; set; }
        [ForeignKey("PacienteId")]
        [DisplayName("Paciente")]
        public virtual Paciente Paciente { get; set; }

        public int AtendimentoId { get; set; }
        [ForeignKey("AtendimentoId")]
        [DisplayName("Atendimento")]
        public virtual Atendimento Atendimento { get; set; }

        [DisplayName("Dt. Entrada")]
        public DateTime DataEntrada { get; set; }

        [DisplayName("Previsão de Alta")]
        public DateTime? PrevisaoAlta { get; set; }

        [DisplayName("Motivo da Internação")]
        public string MotivoInternacao { get; set; }
        public string Leito { get; set; }
        public string Quarto { get; set; }
        public string Setor { get; set; }

        [DisplayName("Plano de Saude Atual.")]
        public string? PlanoSaudeAtualizado { get; set; }

        [DisplayName("Obs. Clínicas")]
        public string ObservacoesClinicas { get; set; }

        [DisplayName("St. de Internação")]
        public string StatusIternacao { get; set; }

        public virtual AltaHospitalar AltaHospitalar { get; set; }
    }
}