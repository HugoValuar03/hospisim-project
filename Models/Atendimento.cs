using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace Hospisim.Models
{
    public class Atendimento
    {
        public int Id { get; set; }

        [DisplayName("Data e Hora")]
        public DateTime DataEHora { get; set; }

        [DisplayName("Tipo de Atendimento")]
        public TipoAtendimento Tipo { get; set; }

        [DisplayName("Status")]
        public Status Status { get; set; }

        [DisplayName("Local")]
        public string Local { get; set; }

        public Guid PacienteId { get; set; }
        [ForeignKey("PacienteId")]

        [DisplayName("Paciente")]
        public virtual Paciente Paciente { get; set; }

        public Guid ProfissionalSaudeId { get; set; }

        [ForeignKey("ProfissionalSaudeId")]
        [DisplayName("Profissional de Saúde")]
        public virtual ProfissionalSaude ProfissionalSaude { get; set; }

        public virtual ICollection<Exame> Exames { get; set; }
        public virtual ICollection<Prescricao> Prescricoes { get; set; }

        public int ProntuarioId { get; set; }

        [ForeignKey("ProntuarioId")]
        [DisplayName("Prontuário")]
        public virtual Prontuario Prontuario { get; set; }
        public virtual Internacao Internacao { get; set; }

        public Atendimento()
        {
            Exames = new HashSet<Exame>();
            Prescricoes = new HashSet<Prescricao>();
        }
    }
}