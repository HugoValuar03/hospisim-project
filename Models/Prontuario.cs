using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hospisim.Models
{
    public class Prontuario
    {
        [Key]
        public int NumeroProntuario { get; set; }
        public DateTime DataAbertura { get; set; }
        public string? ObservacoesGerais { get; set; }

        public Guid PacienteId { get; set; }

        [ForeignKey("PacienteId")]
        public virtual Paciente Paciente { get; set; }

        public virtual ICollection<Atendimento> Atendimentos { get; set; }
        public Prontuario()
        {
            Atendimentos = new HashSet<Atendimento>();
        }
    }
}