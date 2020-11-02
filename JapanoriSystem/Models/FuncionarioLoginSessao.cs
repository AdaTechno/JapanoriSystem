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
        public int FuncionarioID { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        [DisplayName("Nome Completo")]
        public string NomeCompleto
        {
            get
            {
                return Nome + " " + Sobrenome;
            }
        }
        public string Cargo { get; set; }
        public string CPF { get; set; }
        public string Endereco { get; set; }
        public string Cep { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DisplayName("Data de contratação")]
        public DateTime DataContratacao { get; set; }
        [Required]
        [DisplayName("E-mail")]
        [EmailAddress]
        public string EmailCorp { get; set; }
        [Required]
        [DisplayName("Senha")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }
        [Required]
        public string Perm { get; set; }
        [Required]
        public string Status {get; set;}
    }

   
}