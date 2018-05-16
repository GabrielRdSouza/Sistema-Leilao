using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Model {
	[Table("lei.TBL_USUARIO")]
	public class Usuario {
		[Key, Column("US_CODIGO"), DatabaseGenerated(DatabaseGeneratedOption.None)]
		public int Codigo { get; set; }

		[Column("US_NOME"), MaxLength(40)]
		public string Nome { get; set; }

		[Column("US_CPF"), MaxLength(14)]
		public string CPF { get; set; }

		//NULL
		[Column("US_SEXO"), MaxLength(20)]
		public string Sexo { get; set; }

		//NULL
		[Column("US_TELEFONE"), MaxLength(16)]
		public string Telefone { get; set; }

		[Column("US_DATA_CADASTRO")]
		public DateTime DataCadastro { get; set; }

		//NULL
		[Column("US_CIDADE"), MaxLength(40)]
		public string Cidade { get; set; }

		[Column("US_EMAIL"), MaxLength(100)]
		public string Email { get; set; }

		[Column("US_SENHA"), MinLength(8), MaxLength(32)]
		public string Senha { get; set; }
	}
}
