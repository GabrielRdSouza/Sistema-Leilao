using Backend.Model;
using Backend.Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Backend.Repository {
	public class LanceRepository : Repository<Lance> {
		public bool AddLinq(Lance lance) {
			try {
				lance.Codigo = GetLastPK();
				Add(lance);
				return true;
			}
			catch(Exception e) {
				return false;
			}
		}

		public Decimal GetMaiorLance(int cod) {
			try {
				var lances = from l in ctx.Lance
						 .Include("Usuario")
						 .Include("Produto")
							 where l.CodigoProduto == cod
							 orderby l.Valor descending
							 select l;
				return lances.ToList().First().Valor;
			}
			catch(Exception e) {
				return 0;
			}
		}

		public List<Lance> GetUltimoLance(List<Produto> produtos) {
			List<Lance> retorno = new List<Lance>();
			if(produtos.Any()) {
				foreach(Produto produto in produtos) {
					var lista = from l in ctx.Lance
								where l.CodigoProduto == produto.Codigo
								orderby l.Codigo descending
								select l;
					if(lista.Any())
						retorno.Add(lista.First());
					else
						retorno.Add(new Lance());
				}
			}
			return retorno;
		}

		public List<int> GetLast30DaysAdd() {
			List<int> retorno = new List<int>();
			for(DateTime dia = (DateTime.Today.AddDays(-29)); dia <= (DateTime.Today); dia = dia.AddDays(1)) {
				int quantidade = 0;
				var lista = from l in ctx.Lance
							where l.DataCriacao.Equals(dia)
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
