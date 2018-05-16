using Backend.Model;
using Backend.Repository;
using FrontEnd.ViewModel;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace FrontEnd.Controllers {
	public class ProdutoController : ConfiguracaoController {
		ProdutoVM _produtoVM { get; set; }
		LanceRepository _lanceRepository { get; set; }
		ProdutoRepository _produtoRepository { get; set; }
		UsuarioRepository _usuarioRepository { get; set; }

		public ProdutoController() {
			_produtoVM = new ProdutoVM();
			_lanceRepository = new LanceRepository();
			_produtoRepository = new ProdutoRepository();
			_usuarioRepository = new UsuarioRepository();
		}

		// GET: Produto
		[ActionFilter]
		public ActionResult Index() {
			GetSession(_produtoVM);
			_produtoVM.Produtos = ListarUltimosProdutos();
			_produtoVM.UltimoLance = ListarUltimosLances(_produtoVM.Produtos);
			return View(_produtoVM);
		}

		// GET: Leiloar
		[ActionFilter]
		public ActionResult Leiloar() {
			GetSession(_produtoVM);
			return View(_produtoVM);
		}

		[ActionFilter, HttpPost, ValidateAntiForgeryToken]
		public ActionResult CadastrarProduto(ProdutoVM produtoVM) {
			if(_produtoRepository.AddLinq(new Produto {
				CodigoUsuario = (Session["config"] as ConfiguracaoVM).UsuarioLogado.Codigo,
				DataFim = (DateTime.Now.AddDays(7)),
				Descricao = produtoVM.Produto.Descricao,
				Nome = produtoVM.Produto.Nome,
				Novo = produtoVM.Produto.Novo,
				Preco = Convert.ToDecimal(produtoVM.Valor)
			})) {
				GetSession((_produtoVM = produtoVM));
				Mensagem("success", "Sucesso!", "Produto adicionado com sucesso.");
				return RedirectToAction("Index");
			}
			else {
				GetSession((_produtoVM = produtoVM));
				Mensagem("error", "Erro!", "Não foi possível adicionar o produto.");
				return RedirectToAction("Index");
			}
		}

		// GET: Participar
		[ActionFilter]
		public ActionResult Participar(Produto produto) {
			GetSession(_produtoVM);
			_produtoVM.Produto = produto;
			_produtoVM.MaiorLance = _lanceRepository.GetMaiorLance(produto.Codigo).ToString();
			return View(_produtoVM);
		}

		[ActionFilter, HttpPost, ValidateAntiForgeryToken]
		public ActionResult Lance(ProdutoVM produtoVM) {
			if((Convert.ToDecimal(produtoVM.Valor) > _lanceRepository.GetMaiorLance(produtoVM.Produto.Codigo))
				&& _lanceRepository.AddLinq(new Lance {
					Valor = Convert.ToDecimal(produtoVM.Valor),
					DataCriacao = DateTime.Now,
					CodigoProduto = produtoVM.Produto.Codigo,
					CodigoUsuario = (Session["config"] as ConfiguracaoVM).UsuarioLogado.Codigo
				})) {
				GetSession((_produtoVM = produtoVM));
				Mensagem("success", "Parabéns!", "Lance adicionado com sucesso!");
				return RedirectToAction("Index");
			}
			else {
				GetSession((_produtoVM = produtoVM));
				_produtoVM.MaiorLance = _lanceRepository.GetMaiorLance(_produtoVM.Produto.Codigo).ToString();
				Mensagem("error", "Houve um problema!", "Verifique se você digitou um valor maior que o maior lance.");
				return RedirectToAction("Index");
			}
		}

		// GET: Relatório
		[ActionFilter]
		public ActionResult Relatorio() {
			GetSession(_produtoVM);
			_produtoVM.Produtos = _produtoRepository.GetAllEmAberto();
			_produtoVM.Usuarios = _usuarioRepository.GetAllLinq();
			return View(_produtoVM);
		}

		// GET: Gráfico
		[ActionFilter]
		public ActionResult Grafico() {
			GetSession(_produtoVM);
			_produtoVM.QuantidadeLances = _lanceRepository.GetLast30DaysAdd();
			_produtoVM.QuantidadeProdutos = _produtoRepository.GetLast30DaysAdd();
			_produtoVM.QuantidadeUsuarios = _usuarioRepository.GetLast30DaysAdd();
			_produtoVM.Grafico = CriarGrafico(_produtoVM.QuantidadeLances, _produtoVM.QuantidadeProdutos, _produtoVM.QuantidadeUsuarios);
			return View(_produtoVM);
		}

		List<Produto> ListarProdutos() {
			return _produtoRepository.GetAllLinq();
		}
		List<Produto> ListarUltimosProdutos() {
			return _produtoRepository.GetAllEmAberto();
		}

		List<Lance> ListarUltimosLances(List<Produto> produtos) {
			return _lanceRepository.GetUltimoLance(produtos);
		}

		string CriarGrafico(List<int> lances, List<int> produtos, List<int> usuarios) {
			int dia = 1, i = 0;
			string retorno = string.Empty;
			while(i < 30) {
				retorno += $"[{dia}, {lances[i]}, {produtos[i]}, {usuarios[i]}]";
				if(i != 29)
					retorno += ", ";
				dia++;
				i++;
			}
			return retorno;
		}
	}
}