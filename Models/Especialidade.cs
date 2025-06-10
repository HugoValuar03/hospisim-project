using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hospisim.Models
{
    public class Especialidade
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        public virtual ICollection<ProfissionalSaudeEspecialidade> ProfissionalSaudeEspecialidade { get; set; }

        public Especialidade()
        {
            ProfissionalSaudeEspecialidade = new HashSet<ProfissionalSaudeEspecialidade>();
        }
    }
}
