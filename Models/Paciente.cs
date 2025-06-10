using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hospisim.Models
{
    public class Paciente
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = " O Nome do paciente é obrigatório")]
        [StringLength(100, ErrorMessage = "O nome do paciente deve ter no máximo 100 caracteres")]
        public string NomeCompleto { get; set; }

        [Required(ErrorMessage = "O CPF é obrigatório")]
        public string Cpf { get; set; }

        [Required(ErrorMessage = "A data de Nascimento é obrigatória")]
        public DateTime DataNascimento { get; set; }

        [Required(ErrorMessage = "O Sexo é obrigatório")]
        public string Sexo { get; set; }

        [Required(ErrorMessage = "O tipo sanguíneo é obrigatório")]
        public string TipoSanguineo { get; set; }

        public string Telefone { get; set; }

        [Required(ErrorMessage = "O endereço é obrigatório")]
        public string EnderecoCompleto { get; set; }

        public string NumeroCartaoSus { get; set; }
        [Required(ErrorMessage = "O estado civil é obrigatório")]
        public string EstadoCivil { get; set; }

        public bool PossuiPlanoSaude { get; set; }

        public virtual ICollection<Prontuario> Prontuarios { get; set; }
        public virtual ICollection<Atendimento> Atendimentos { get; set; }

        public Paciente()
        {
            Prontuarios = new HashSet<Prontuario>();
            Atendimentos = new HashSet<Atendimento>();
        }
    }
}