using Backend.Model;

namespace FrontEnd.ViewModel {
	public class ConfiguracaoVM {
		//Segurança
		public Usuario UsuarioLogado { get; set; }
		
		//Personalização
		public string Controller { get; set; }
		public string Action { get; set; }

		//Flags
		public bool PerdeuSession { get; set; }

		public ConfiguracaoVM() {
			PerdeuSession = false;
		}

	}
}