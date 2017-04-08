using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace w1Consultorio.Models
{
    public class Prontuario
    {
        [Key]
        public int CodProntuario { get; set; }
        public int CodAtendimento { get; set; }

        [Required(ErrorMessage = "Informe o tipo do exame")]
        [Display(Name = "Tipo do exame:")]
        public int CodTipoExame { get; set; }

        [Required(ErrorMessage = "Informe a empresa")]
        [Display(Name = "Empresa:")]
        public int CodEmpresa { get; set; }

        [Required(ErrorMessage = "Informe o departamento")]
        [Display(Name = "Departamento:")]
        public int CodDepartamento { get; set; }

        [Required(ErrorMessage = "Informe o cargo")]
        [Display(Name = "Cargo:")]
        public int CodCargo { get; set; }

        [Required(ErrorMessage = "Informe o funcionário")]
        [Display(Name = "Funcionário:")]
        public int CodFuncionario { get; set; }

        [Required(ErrorMessage = "Informe a data do exame")]
        [Display(Name = "Data do exame:")]
        public DateTime dataExame { get; set; }

        [Required(ErrorMessage = "Informe o resultado do exame clínico")]
        [Display(Name = "Exame Clínico:")]
        public int ExameClinico { get; set; }

        [Required(ErrorMessage = "Informe o resultado do exame")]
        [Display(Name = "Resultado do Exame:")]
        public int ExameResultado { get; set; }

        [Required(ErrorMessage = "Informe o médico responsável")]
        [Display(Name = "Médico Responsável")]
        public int codProfissional { get; set; }

        [Display(Name = "Estado civil")]
        public int EstadoCivil {get; set;}

        [Display(Name = "ASO gerado")]
        public int Aso { get; set; }

        [Display(Name = "Observação ASO")]
        public string AsoObservacao { get; set; }

        [Required(ErrorMessage ="Informe o campo Exame considerado")]
        [Display(Name = "Exame considerado")]
        public int CodResultadoClinico { get; set; } 

        [Required(ErrorMessage = "Informe o campo Resultado do exame")]
        [Display(Name = "Resultado do Exame")]
        public int CodResultadoProntuario { get; set; }

        public virtual Empresa Empresa { get; set; }
        public virtual Departamento Departamento { get; set; }
        public virtual Cargo Cargo { get; set;}
        public virtual Funcionario Funcionario { get; set; }
        public virtual Profissional Profissional { get; set; }
        public virtual TipoExame TipoExame { get; set; }

        [NotMapped]
        public ResultadoClinico ResultadoClinico { get; set; }
        [NotMapped]
        public ResultadoProntuario ResultadoProntuario { get; set; }

    }
}