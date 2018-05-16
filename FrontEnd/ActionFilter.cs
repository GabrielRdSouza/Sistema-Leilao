using FrontEnd.ViewModel;
using System.Web.Mvc;

namespace FrontEnd {
	public class ActionFilter : ActionFilterAttribute {
		ConfiguracaoVM _config;
		void GetSession(ConfiguracaoVM session) {
			if(session != null)
				_config = session;
			else
				_config = new ConfiguracaoVM();
		}
		public override void OnActionExecuting(ActionExecutingContext filterContext) {
			GetSession(filterContext.HttpContext.Session.Contents["config"] as ConfiguracaoVM);
			string controller = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
			_config.Action = filterContext.ActionDescriptor.ActionName;
			_config.Controller = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;

			//Caso não esteja logado:
			if(_config.UsuarioLogado == null) {
				filterContext.Result = new RedirectResult(Configuration.Login);
				_config.PerdeuSession = true;
			}

			filterContext.HttpContext.Session.Contents["config"] = _config;
		}

	}
}