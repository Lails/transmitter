using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Lails.Transmitter.Retriever
{
	public abstract class BaseRetriver<TEntity, TDbContext>
		where TDbContext : DbContext
		where TEntity : class
	{
		protected IQueryable<TEntity> Query { get; private set; }
		internal void SetQuery(IQueryable<TEntity> query)
		{
			Query = query;
		}

		public abstract IQueryable<TEntity> QueryDefinition();
	}
}
