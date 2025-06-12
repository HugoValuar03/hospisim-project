using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace Hospisim.Models
{
    public class AltaHospitalar
    {
        [Key]
        public Guid InternacaoId { get; set; }

        [DisplayName("Data")]
        public DateTime Data { get; set; }

        [DisplayName("Condição do Paciente")]
        public string CondicaoPaciente { get; set; }

        [DisplayName("Instruções pós Alta")]
        public string InstrucoesPosAlta { get; set; }

        [DisplayName("Internação")]
        public virtual Internacao? Internacao { get; set; }
    }
}