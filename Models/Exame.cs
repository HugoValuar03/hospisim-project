using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Hospisim.Models
{
    public class Exame
    {
        public Guid Id { get; set; }

        [DisplayName("Tipo de Exame")]
        public string TipoExame { get; set; }

        [DisplayName("Data da Solicitação")]
        public DateTime DataSolicitacao { get; set; }

        [DisplayName("Data da Realização")]
        public DateTime DataRealizacao { get; set; }

        [DisplayName("Resultado")]
        public string Resultado { get; set; }

        public int AtendimentoId { get; set; }
        [ForeignKey("AtendimentoId")]
        [DisplayName("Atendimento")]
        public virtual Atendimento Atendimento { get; set; }
    }
}