using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Model {
	[Table("lei.TBL_PRODUTO")]
	public class Produto {
		[Key, Column("PR_CODIGO"), DatabaseGenerated(DatabaseGeneratedOption.None)]
		public int Codigo { get; set; }

		[Column("PR_NOME"), MaxLength(40)]
		public string Nome { get; set; }

		//NULL
		[Column("PR_DESCRICAO"), MaxLength(500)]
		public string Descricao { get; set; }

		[Column("PR_PRECO")]
		public Decimal Preco { get; set; }

		[Column("PR_NOVO")]
		public bool Novo { get; set; }

		[Column("PR_DATA_FIM")]
		public DateTime DataFim { get; set; }

		[Column("US_CODIGO_USUARIO")]
		public int CodigoUsuario { get; set; }
		[ForeignKey("CodigoUsuario")]
		public Usuario Usuario { get; set; }
	}
}
