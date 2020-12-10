using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace JapanoriSystem.Models
{

    // Relacionamento Funcionario - Usuario - Login - Senha


    [Table("tbFuncionario")]
    public class Funcionario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int FuncionarioID { get; set; } //       ID do funcionário
        [StringLength(50)]
        public string Nome { get; set; } //         Nome
        [StringLength(100)]
        public string Sobrenome { get; set; } //        Sobrenome
        [DisplayName("Nome Completo")]
        public string NomeCompleto
        {
            get
            {
                return Nome + " " + Sobrenome; //           Nome Completo
            }
        }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DisplayName("Data de nascimento")]
        public DateTime DataNasc { get; set; } //       Data de Nascimento

        [StringLength(50)]
        public string Cargo { get; set; } //    Cargo

        [StringLength(14)]
        public string CPF { get; set; } //      CPF

        public string Endereco { get; set; } //     Endereço

        public string NumeroEnd { get; set; } //        Número do Endereço

        [StringLength(10)]
        public string Cep { get; set; } //      CEP

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DisplayName("Data de contratação")]
        public DateTime DataContratacao { get; set; } //        Data da Contratação

        [Required]
        [DisplayName("E-mail")]
        [EmailAddress]
        public string EmailCorp { get; set; } //        Email Corporativo

        [Required]
        [DisplayName("Senha")]
        [DataType(DataType.Password)]
        public string Senha { get; set; } //        Senha

        [Required]
        public string Perm { get; set; } //         Permissão

        [Required]
        [DisplayName("Status")]
        public string cStatus {get; set;} //         cStatus (Lixeira)
    }

   
}