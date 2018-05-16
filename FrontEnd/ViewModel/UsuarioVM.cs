using Backend.Model;

namespace FrontEnd.ViewModel {
	public class UsuarioVM : ConfiguracaoVM {
		public Usuario Usuario { get; set; }




		public UsuarioVM() {
			Usuario = new Usuario();
		}
	}
}