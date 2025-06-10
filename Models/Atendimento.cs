using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace Hospisim.Models
{
    public class Atendimento
    {
        public int Id { get; set; }
        public DateTime DataEHora { get; set; }
        public TipoAtendimento Tipo { get; set; }

        public Status Status { get; set; }

        public string Local { get; set; }

        public Guid PacienteId { get; set; }
        [ForeignKey("PacienteId")]
        public virtual Paciente Paciente { get; set; }

        public Guid ProfissionalSaudeId { get; set; }
        [ForeignKey("ProfissionalSaudeId")]
        public virtual ProfissionalSaude ProfissionalSaude { get; set; }

        public virtual ICollection<Exame> Exames { get; set; }
        public virtual ICollection<Prescricao> Prescricoes { get; set; }

        public int ProntuarioId { get; set; }

        [ForeignKey("ProntuarioId")]
        public virtual Prontuario Prontuario { get; set; }
        public virtual Internacao Internacao { get; set; }

        public Atendimento()
        {
            Exames = new HashSet<Exame>();
            Prescricoes = new HashSet<Prescricao>();
        }
    }
}