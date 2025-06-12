using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hospisim.Models
{
    public class Paciente
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = " O Nome do paciente é obrigatório")]
        [StringLength(100, ErrorMessage = "O nome do paciente deve ter no máximo 100 caracteres")]
        [DisplayName("Nome Completo")]
        public string NomeCompleto { get; set; }

        [Required(ErrorMessage = "O CPF é obrigatório")]
        [DisplayName("CPF")]
        public string Cpf { get; set; }

        [Required(ErrorMessage = "A data de Nascimento é obrigatória")]
        [DisplayName("Dt. Nascimento")]
        public DateTime DataNascimento { get; set; }

        [Required(ErrorMessage = "O Sexo é obrigatório")]
        [DisplayName("Sexo")]
        public string Sexo { get; set; }

        [Required(ErrorMessage = "O tipo sanguíneo é obrigatório")]
        [DisplayName("Tipo Sanguíneo")]
        public string TipoSanguineo { get; set; }

        [DisplayName("Telefone")]
        public string Telefone { get; set; }

        [Required(ErrorMessage = "O endereço é obrigatório")]
        [DisplayName("End. Completo")]
        public string EnderecoCompleto { get; set; }

        [DisplayName("Nº Cartão SUS")]
        public string NumeroCartaoSus { get; set; }
        [Required(ErrorMessage = "O estado civil é obrigatório")]

        [DisplayName("Estado Civíl")]
        public string EstadoCivil { get; set; }

        [DisplayName("Plano de Saúde")]
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