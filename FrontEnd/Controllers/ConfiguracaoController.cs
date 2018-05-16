using Backend.Model;
using FrontEnd.ViewModel;
using System.Web.Mvc;

namespace FrontEnd.Controllers {
	public class ConfiguracaoController : Controller {
		protected ConfiguracaoVM ConfiguracaoVM { get; set; }

		/// <summary>
		/// Atribui um Usuário à Session
		/// </summary>
		protected void SetSession(Usuario usuario) {
			if(Session["config"] != null)
				(Session["config"] as ConfiguracaoVM).UsuarioLogado = usuario;
			else
				Session["config"] = new ConfiguracaoVM { UsuarioLogado = usuario };
		}

		/// <summary>
		/// Busca na Session todas as props com valor e atribui na VM filha para que sejam mostradas as informações na View.
		/// </summary>
		protected ConfiguracaoVM GetSession(ConfiguracaoVM configuracaoVM) {
			if(configuracaoVM != null)
				ConfiguracaoVM = configuracaoVM;
			else
				ConfiguracaoVM = new ConfiguracaoVM();

			ConfiguracaoVM session;
			if(Session["config"] != null)
				session = Session["config"] as ConfiguracaoVM;
			else
				session = new ConfiguracaoVM();

			ConfiguracaoVM.UsuarioLogado = session.UsuarioLogado;
			ConfiguracaoVM.Action = session.Action;
			ConfiguracaoVM.Controller = session.Controller;
			ConfiguracaoVM.PerdeuSession = session.PerdeuSession;

			return ConfiguracaoVM;
		}

		protected void KillSession() {
			if(Session["config"] != null)
				Session["config"] = null;
		}

		/// <summary>
		/// tipo: success, error, info, warning
		/// </summary>
		protected void Mensagem(string tipo, string titulo, string mensagem) {
			TempData["Mensagem"] = mensagem;
			TempData["Tipo"] = tipo;
			TempData["Titulo"] = titulo;
		}
	}
}