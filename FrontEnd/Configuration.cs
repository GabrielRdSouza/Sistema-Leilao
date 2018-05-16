using System.Reflection;

namespace FrontEnd {
	public static class Configuration {

		//Redirecionamentos
		public static string Login => $"{Sistema}/Login";
		public static string CadastroUsuario => $"{Sistema}/Cadastro/Index";
		public static string Produto => $"{Sistema}/Produto";

		//Estrutura do sistema Web
		public static string Protocolo => "http";
		public static string Endereco => "localhost";
		public static string Porta => "2677";
		public static string Sistema => $"{Protocolo}://{Endereco}:{Porta}";

		//Versão do sistema
		public static string Versao => Assembly.GetExecutingAssembly().GetName().Version.ToString();
	}
}