using Backend.Model;
using Backend.Repository;
using FrontEnd.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace FrontEnd.Controllers {
	public class CadastroController : ConfiguracaoController {
		UsuarioVM _usuarioVM;
		UsuarioRepository _usuarioRepository;

		public CadastroController() {
			_usuarioVM = new UsuarioVM();
			_usuarioRepository = new UsuarioRepository();
		}

		// GET: Cadastro
		public ActionResult Index() {
			GetSession(_usuarioVM);
			return View(_usuarioVM);
		}

		[ValidateAntiForgeryToken, HttpPost]
		public ActionResult Cadastrar(Usuario usuario) {
			if(_usuarioRepository.AddLinq(new Usuario {
				CPF = usuario.CPF, Cidade = usuario.Cidade,
				DataCadastro = DateTime.Now, Email = usuario.Email,
				Nome = usuario.Nome, Senha = usuario.Senha = usuario.Senha,
				Sexo = usuario.Sexo, Telefone = usuario.Telefone
			})) {
				Mensagem("success", "Sucesso!", "Cadastrado com sucesso.");
				GetSession(_usuarioVM);
				_usuarioVM.Usuario = usuario;
				return RedirectToAction("Index", "Login");
			}
			else {
				GetSession(_usuarioVM);
				Mensagem("error", "Erro!", "Informações inválidas!");
				return View("Index", _usuarioVM);
			}
		}

		[HttpPost]
		public ActionResult ValidarCPF(string CPF) {
			bool retorno = false;
			if(!string.IsNullOrEmpty(CPF)) {
				if((_usuarioRepository.Get(buscar => buscar.CPF.Equals(CPF))).ToList().Any())
					retorno = true;
			}
			return Json(retorno, JsonRequestBehavior.AllowGet);
		}

		[HttpPost]
		public ActionResult ValidarEmail(string Email) {
			bool retorno = false;
			if(!string.IsNullOrEmpty(Email)) {
				if((_usuarioRepository.Get(buscar => buscar.Email.Equals(Email))).ToList().Any())
					retorno = true;
			}
			return Json(retorno, JsonRequestBehavior.AllowGet);
		}

		[HttpPost]
		public ActionResult ValidarTelefone(string Telefone) {
			bool retorno = false;
			if(!string.IsNullOrEmpty(Telefone)) {
				List<Usuario> usuarios = _usuarioRepository.GetAll().ToList();
				foreach(var item in usuarios)
					if(item.Telefone != null)
						if(item.Telefone.Equals(Telefone))
							retorno = true;
			}
			return Json(retorno, JsonRequestBehavior.AllowGet);
		}
	}
}