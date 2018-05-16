using Backend.Model;
using System.Collections.Generic;

namespace FrontEnd.ViewModel {
	public class ProdutoVM : ConfiguracaoVM {
		public Lance Lance { get; set; }
		public Produto Produto { get; set; }
		public List<Produto> Produtos { get; set; }
		public List<Usuario> Usuarios { get; set; }
		public List<Lance> UltimoLance { get; set; }
		public List<int> QuantidadeLances { get; set; }
		public List<int> QuantidadeProdutos { get; set; }
		public List<int> QuantidadeUsuarios { get; set; }
		public string Grafico { get; set; }
		public string Valor { get; set; }
		public string MaiorLance { get; set; }

		public ProdutoVM() {
			Lance = new Lance();
			Produto = new Produto();
			Produtos = new List<Produto>();
			UltimoLance = new List<Lance>();
			Usuarios = new List<Usuario>();
		}
	}
}