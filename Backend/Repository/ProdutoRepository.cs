using Backend.Model;
using Backend.Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Backend.Repository {
	public class ProdutoRepository : Repository<Produto> {

		public bool AddLinq(Produto produto) {
			try {
				produto.Codigo = GetLastPK();
				Add(produto);
				return true;
			}
			catch(Exception e) {
				return false;
			}
		}

		public List<Produto> GetAllLinq() {
			var produto = from p in ctx.Produto
						  .Include("Usuario")
						  orderby p.Codigo descending
						  select p;
			return produto.ToList();
		}

		public List<Produto> GetAllEmAberto() {
			var produto = from p in ctx.Produto
						  .Include("Usuario")
						  where p.DataFim > DateTime.Today
						  orderby p.Codigo descending
						  select p;
			return produto.ToList();
		}

		public List<int> GetLast30DaysAdd() {
			List<int> retorno = new List<int>();
			for(DateTime dia = (DateTime.Today.Date.AddDays(-22)); dia <= (DateTime.Today.AddDays(7)); dia = dia.AddDays(1)) {
				int quantidade = 0;
				var lista = from l in ctx.Produto
							where l.DataFim.Equals(dia)
							select l;
				if(lista.Any()) {
					quantidade = lista.Count();
				}
				retorno.Add(quantidade);
			}
			return retorno;
		}

		int GetLastPK() {
			var objeto = GetAll().OrderBy(ordenarPor => ordenarPor.Codigo).ToList();
			if(objeto.Any())
				return (1 + objeto.Last().Codigo);
			return 1;
		}
	}
}
