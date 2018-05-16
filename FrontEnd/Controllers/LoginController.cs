using Backend.Model;
using Backend.Repository;
using FrontEnd.ViewModel;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace FrontEnd.Controllers {
	public class LoginController : ConfiguracaoController {
		UsuarioVM _usuarioVM;
		UsuarioRepository _usuarioRepository;

		public LoginController() {
			_usuarioVM = new UsuarioVM();
			_usuarioRepository = new UsuarioRepository();
		}

		// GET: Login
		public ActionResult Index() {
			GetSession(_usuarioVM);
			if(_usuarioVM.PerdeuSession)
				Mensagem("info", "Sessão expirada!", "Por favor, para continuar, entre novamente.");
			return View(_usuarioVM);
		}

		[ValidateAntiForgeryToken, HttpPost]
		public ActionResult Logar(UsuarioVM usuarioVM) {
			List<Usuario> response = _usuarioRepository.Get(
				buscar => (buscar.CPF == usuarioVM.Usuario.CPF && 
				buscar.Senha == usuarioVM.Usuario.Senha)).ToList();
			if(response.Any()) {
				SetSession(response.First());
				//Caso tivessem várias controllers disponíveis, aqui haveria um redirecionamento
				//para a controller que o usuári tentou acessar antes de logar
				if(!string.IsNullOrEmpty((Session["config"] as ConfiguracaoVM).Controller))
					return RedirectToAction("Index", (Session["config"] as ConfiguracaoVM).Controller);
				return RedirectToAction("Index","Produto");
			}
			_usuarioVM = usuarioVM;
			GetSession(_usuarioVM);
			Mensagem("error", "Erro!", "Usuário ou senha inválidos.");
			return RedirectToAction("Index");
		}

		// GET: Deslogar
		public ActionResult Deslogar() {
			KillSession();
			return RedirectToAction("Index");
		}
	}
}