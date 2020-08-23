using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Lails.Transmitter.Retriever
{
	public interface IRetriever<TEntity, TDbContext>
		where TEntity : class
		where TDbContext : DbContext
	{
		Task<TEntity> GetById(object id);
		Task<List<TEntity>> GetByAction(Expression<Func<TEntity, bool>> predicate);
		Task<List<TEntity>> GetByFilterAsync(BaseRetriver<TEntity, TDbContext> retriver);
	}
}
