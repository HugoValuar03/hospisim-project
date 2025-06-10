using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema; // Certifique-se de que está aqui para ForeignKey
using System.ComponentModel.DataAnnotations; // Certifique-se de que está aqui para Key

namespace Hospisim.Models
{
    public class Internacao
    {
        public Guid Id { get; set; }

        public Guid PacienteId { get; set; }
        [ForeignKey("PacienteId")]
        public virtual Paciente Paciente { get; set; }

        public int AtendimentoId { get; set; }
        [ForeignKey("AtendimentoId")]
        public virtual Atendimento Atendimento { get; set; }

        public DateTime DataEntrada { get; set; }
        public DateTime? PrevisaoAlta { get; set; }
        public string MotivoInternacao { get; set; }
        public string Leito { get; set; }
        public string Quarto { get; set; }
        public string Setor { get; set; }
        public string? PlanoSaudeAtualiziado { get; set; }
        public string ObservacoesClinicas { get; set; }
        public string StatusIternacao { get; set; }

        // Propriedade de Navegação para AltaHospitalar (0..1:1, pode ser nula)
        public virtual AltaHospitalar AltaHospitalar { get; set; }
    }
}