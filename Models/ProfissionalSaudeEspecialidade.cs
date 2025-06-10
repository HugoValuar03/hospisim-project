using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Hospisim.Models
{
    public class ProfissionalSaudeEspecialidade
    {
        public Guid ProfissionalSaudeId { get; set; }
        public int EspecialidadeId { get; set; }

        [ForeignKey("ProfissionalSaudeId")]
        public virtual ProfissionalSaude ProfissionalSaude { get; set; }

        [ForeignKey("EspecialidadeId")]
        public virtual Especialidade Especialidade { get; set; }
    }
}