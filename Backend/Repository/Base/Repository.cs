using Backend.Context;
using Backend.Interface;
using System;
using System.Data.Entity;
using System.Linq;

namespace Backend.Repository.Base {
	public abstract class Repository<TEntity> : IDisposable, IRepository<TEntity> where TEntity : class {
		protected ContextModel ctx;
		
		public Repository() {
			ctx = new ContextModel();
		}

		public void Add(TEntity obj) {
			ctx.Set<TEntity>().Add(obj);
			SaveAll();
		}

		public void Delete(Func<TEntity, bool> predicate) {
			ctx.Set<TEntity>().Where(predicate).ToList().ForEach(del => ctx.Set<TEntity>().Remove(del));
			SaveAll();
		}

		public void Dispose() {
			ctx.Dispose();
		}

		public TEntity Find(params object[] obj) {
			return ctx.Set<TEntity>().Find(obj);
		}

		public IQueryable<TEntity> Get(Func<TEntity, bool> predicate) {
			return GetAll().Where(predicate).AsQueryable();
		}

		public IQueryable<TEntity> GetAll() {
			return ctx.Set<TEntity>();
		}

		public void SaveAll() {
			ctx.SaveChanges();
		}

		public void Update(Func<TEntity, bool> predicate, TEntity obj) {
			ctx = new ContextModel();
			var atualizar = ctx.Set<TEntity>().AsNoTracking().FirstOrDefault(predicate);
			atualizar = obj;
			ctx.Set<TEntity>().Attach(obj);
			ctx.Entry(atualizar).State = EntityState.Modified;
			ctx.Entry(obj).State = EntityState.Modified;
			SaveAll();
		}
	}
}
