using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace w1Consultorio.Models
{
    public class Funcionario
    {
        [Key]
        public Int32 CodFuncionario { get; set; }
        [Required(ErrorMessage = "Informe o nome do funcionário")]
        [Display(Name = "Nome do Funcionário")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Informe o cargo do funcionário")]
        public Int32 CodCargo { get; set; }
        [Required(ErrorMessage = "Informe o estado civil")]
        public Int32 CodEstCivil { get; set; }
        [Required(ErrorMessage = "Informe o situação do funcionário")]
        public Int32 CodSituacao { get; set; }
        [Required(ErrorMessage = "Informe o periodicidade")]
        public Int32 CodPeriodicidade { get; set; }
        public string Rg { get; set; }
        public string Sexo { get; set; }
        [Display(Name = "Cargo Anterior")]
        public string CargoAnterior { get; set; }
        public string Natural { get; set; }
        public string Nacionalidade { get; set; }
        [Required(ErrorMessage = "Informe a empresa")]
        public int CodEmpresa { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date, ErrorMessage="Data de nascimento inválida")]
        [Display(Name = "Data nascimento")]
        public DateTime? DataNascimento { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date, ErrorMessage="Data de admissão inválida")]
        [Display(Name = "Data admissão")]
        public DateTime? DataAdmissao { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date, ErrorMessage="Data do último exame inválida")]
        [Display(Name = "Data último exame")]
        public DateTime? DataUltimoExame { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date, ErrorMessage="Data de demissão inválida")]
        [Display(Name = "Data demissão")]
        public DateTime? DataDemissao { get; set; }

        [NotMapped]
        public string Situacao
        {
            get
            {
                string _situacao = "-";
                if (CodSituacao == 1)
                    _situacao = "Ativo";
                if (CodSituacao == 2)
                    _situacao = "Inativo";
                if (CodSituacao == 3)
                    _situacao = "Demitido";
                return _situacao;
            }
        }

        [NotMapped]
        public string SexoDescricao
        {
            get
            {
                string _sexo = "-";
                if (Sexo == "M")
                    _sexo = "Masculino";
                if (Sexo == "F")
                    _sexo = "Feminino";
                return _sexo;
            }
        }
        public virtual Cargo Cargo { get; set; }
        public virtual EstadoCivil EstadoCivil { get; set; }
        public virtual Periodicidade Periodicidade { get; set; }
    }
}