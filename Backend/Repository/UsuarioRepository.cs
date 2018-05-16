using Backend.Model;
using Backend.Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Backend.Repository {
	public class UsuarioRepository : Repository<Usuario> {

		public bool AddLinq(Usuario usuario) {
			try {
				usuario.Codigo = GetLastPK();
				Add(usuario);
				return true;
			}
			catch(Exception e) {
				return false;
			}
		}

		public List<Usuario> GetAllLinq() {
			var usuario = from u in ctx.Usuario
						  orderby u.Nome ascending
						  select u;
			return usuario.ToList();
		}

		public List<int> GetLast30DaysAdd() {
			List<int> retorno = new List<int>();
			for(DateTime dia = (DateTime.Today.AddDays(-29)); dia <= (DateTime.Today); dia = dia.AddDays(1)) {
				int quantidade = 0;
				var lista = from l in ctx.Usuario
							where l.DataCadastro.Equals(dia)
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
