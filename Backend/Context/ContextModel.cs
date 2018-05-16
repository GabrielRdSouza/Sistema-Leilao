using Backend.Model;
using System.Data.Entity;

namespace Backend.Context {
	public class ContextModel : DbContext {
		public ContextModel() : base("name=LeilaoConnection") {
			Configuration.LazyLoadingEnabled = false;
		}

		public virtual DbSet<Usuario> Usuario { get; set; }
		public virtual DbSet<Produto> Produto { get; set; }
		public virtual DbSet<Lance> Lance { get; set; }
	}
}
