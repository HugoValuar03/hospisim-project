using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Hospisim.Models
{
    public class Exame
    {
        public Guid Id { get; set; }
        public string TipoExame { get; set; }
        public DateTime DataSolicitacao { get; set; }
        public DateTime DataRealizacao { get; set; }
        public string Resultado { get; set; }

        public int AtendimentoId { get; set; }
        [ForeignKey("AtendimentoId")]
        public virtual Atendimento Atendimento { get; set; }
    }
}