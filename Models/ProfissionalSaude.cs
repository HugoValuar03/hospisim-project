using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hospisim.Models
{
    public class ProfissionalSaude
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "O Nome do profissional de saúde é obrigatório")]
        public string NomeCompleto { get; set; }

        [Required(ErrorMessage = "O CPF é obrigatório")]
        public string Cpf { get; set; }

        [Required(ErrorMessage = "O telefone é obrigatório")]
        public string Telefone { get; set; }

        [Required(ErrorMessage = "O Registro conselho é obrigatório")]
        public string RegistroConselho { get; set; }

        [Required(ErrorMessage = "O tipo do registro é obrigatória")]
        public string TipoRegistro { get; set; }

        public DateTime DataAdmissao { get; set; }

        public int CargaHorarioSemanal { get; set; }

        public string Turno { get; set; }
        public bool Ativo { get; set; }

        public virtual ICollection<Atendimento> Atendimentos { get; set; }
        public virtual ICollection<ProfissionalSaudeEspecialidade> ProfissionalSaudeEspecialidades { get; set; }

        public ProfissionalSaude()
        {
            Atendimentos = new HashSet<Atendimento>();
            ProfissionalSaudeEspecialidades = new HashSet<ProfissionalSaudeEspecialidade>();
        }
    }
}
