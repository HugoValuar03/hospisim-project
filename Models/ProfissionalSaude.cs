using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hospisim.Models
{
    public class ProfissionalSaude
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "O Nome do profissional de saúde é obrigatório")]
        [DisplayName("Nome Completo")]
        public string NomeCompleto { get; set; }

        [Required(ErrorMessage = "O CPF é obrigatório")]
        [DisplayName("CPF")]
        public string Cpf { get; set; }

        [Required(ErrorMessage = "O telefone é obrigatório")]
        public string Telefone { get; set; }

        [Required(ErrorMessage = "O Registro conselho é obrigatório")]
        [DisplayName("Registro Conselho")]
        public string RegistroConselho { get; set; }

        [Required(ErrorMessage = "O tipo do registro é obrigatória")]
        [DisplayName("Tipo Registro")]
        public string TipoRegistro { get; set; }

        [DisplayName("Dt. Admissão")]
        public DateTime DataAdmissao { get; set; }

        [DisplayName("Carga Horario Semanal")]
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
