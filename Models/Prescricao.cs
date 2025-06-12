using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace Hospisim.Models
{
    public class Prescricao
    {
        public Guid Id { get; set; }
        public int AtendimentoId { get; set; }
        [ForeignKey("AtendimentoId")]
        [DisplayName("Atendimento")]
        public virtual Atendimento? Atendimento { get; set; }
        public Guid ProfissionalSaudeId { get; set; }
        [ForeignKey("ProfissionalSaudeId")]
        [DisplayName("Profis. de Saúde")]
        public virtual ProfissionalSaude? ProfissionalSaude { get; set; }
        public string Medicamento { get; set; }
        public string Dosagem { get; set; }
        public string Frequencia { get; set; }
        [DisplayName("Via Administração")]
        public string ViaAdministracao { get; set; }
        [DisplayName("Dt. Início")]
        public DateTime DataInicio { get; set; }
        [DisplayName("Dt. Fim")]
        public DateTime? DataFim { get; set; }
        public string Observacoes { get; set; }
        [DisplayName("Status Prescrição")]
        public string StatusPrescricao { get; set; }
        [DisplayName("Reações Adversas")]
        public string? ReacoesAdversas { get; set; }
    }
}