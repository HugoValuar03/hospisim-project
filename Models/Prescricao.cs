using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hospisim.Models
{
    public class Prescricao
    {
        public Guid Id { get; set; }
        public int AtendimentoId { get; set; }
        [ForeignKey("AtendimentoId")]
        public virtual Atendimento Atendimento
        { get; set; }
        public Guid ProfissionalSaudeId { get; set; }
        [ForeignKey("ProfissionalSaudeId")]
        public virtual ProfissionalSaude ProfissionalSaude { get; set; }
        public string Medicamento { get; set; }
        public string Dosagem { get; set; }
        public string Frequencia { get; set; }
        public string ViaAdministracao { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
        public string Observacoes { get; set; }
        public string StatusPrescricao { get; set; }
        public string? ReacoesAdversas { get; set; }
    }
}