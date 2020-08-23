using Lails.Transmitter.UtilityClasses;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Lails.Transmitter.Retriever
{
	public sealed class Retriver<TEntity, TDbContext> : IRetriever<TEntity, TDbContext>
		where TEntity : class
		where TDbContext : DbContext
	{
		readonly TDbContext _context;
		readonly bool _asNoTracking;
		public Retriver(TDbContext context, bool asNoTracking)
		{
			_context = context;
			_asNoTracking = asNoTracking;
		}

		public async Task<TEntity> GetById(object id)
		{
			var result = await _context.FindAsync<TEntity>(id);

			if (_asNoTracking == true)
			{
				_context.Entry(result).State = EntityState.Detached;
			}			

			return result;
		}

		public async Task<List<TEntity>> GetByAction(Expression<Func<TEntity, bool>> predicate)
		{
			if (predicate == null)
			{
				predicate = r => true;
			}

			var query = _asNoTracking
				? _context.Set<TEntity>().AsNoTracking()
				: _context.Set<TEntity>().AsQueryable();

			var result = await query.Where(predicate).ToListAsync();
			return result;
		}

		public async Task<List<TEntity>> GetByFilterAsync(BaseRetriver<TEntity, TDbContext> retriver)
		{
			var query = _asNoTracking
				? _context.Set<TEntity>().AsNoTracking()
				: _context.Set<TEntity>().AsQueryable();

			if (retriver != null)
			{
				retriver.SetQuery(query);
				query = retriver.QueryDefinition();
			}

			var result = await query.ToListAsync();
			return result;
		}
	}
}
