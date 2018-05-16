using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Model {
	[Table("lei.TBL_LANCE")]
	public class Lance {
		[Key, Column("LA_CODIGO"), DatabaseGenerated(DatabaseGeneratedOption.None)]
		public int Codigo { get; set; }

		[Column("LA_VALOR")]
		public Decimal Valor { get; set; }

		[Column("LA_DATA_CRIACAO")]
		public DateTime DataCriacao { get; set; }

		
		//FKs
		[Column("PR_CODIGO_PRODUTO")]
		public int CodigoProduto { get; set; }
		[ForeignKey("CodigoProduto")]
		public Produto Produto { get; set; }

		[Column("US_CODIGO_USUARIO")]
		public int CodigoUsuario { get; set; }
		[ForeignKey("CodigoUsuario")]
		public Usuario Usuario { get; set; }
	}
}
