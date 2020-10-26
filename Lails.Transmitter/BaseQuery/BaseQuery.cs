using Lails.Transmitter.CrudOperations;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lails.Transmitter.BaseQuery
{
	public abstract class BaseQuery<TEntity, TFilter, TDbContext> : BaseCrudOperations<TDbContext>
		where TEntity : class
		where TDbContext : DbContext
		where TFilter : IQueryFilter
	{
		protected IQueryable<TEntity> Query { get; }

		public abstract IQueryable<TEntity> QueryDefinition(ref IQueryable<TEntity> query);
		public abstract IQueryable<TEntity> QueryFilter(ref IQueryable<TEntity> query, TFilter filter);

		internal TFilter Filter { get; private set; }
		internal bool IsAsNoTracking { get; private set; }

		public async Task<List<TEntity>> ApplyFilterAsNoTraking(TFilter filter)
		{
			IsAsNoTracking = true;

			return await ApplyFilter(filter);
		}
		public async Task<List<TEntity>> ApplyFilter(TFilter filter)
		{
			Filter = filter;

			return await _dbCRUD.GetByFilterAsync(this);
		}
	}
}

