using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hospisim.Models
{
    public class AltaHospitalar
    {
        [Key]
        [ForeignKey("Internacao")]
        public Guid InternacaoId { get; set; }

        public DateTime Data { get; set; }
        public string CondicaoPaciente { get; set; }
        public string InstrucoesPosAlta { get; set; }
        public virtual Internacao Internacao { get; set; }
    }
}