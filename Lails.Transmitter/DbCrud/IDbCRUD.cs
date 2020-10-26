using Lails.Transmitter.BaseQuery;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lails.Transmitter.DbCrud
{
	internal interface IDbCRUD<TDbContext>
		where TDbContext : DbContext
	{
		Task CreateAsync<TData>(TData data) where TData : class;
		Task UpdateAsync<TData>(TData data) where TData : class;
		Task DeleteAsync<TData>(TData data) where TData : class;


		Task<List<TEntity>> GetByFilterAsync<TEntity, TFilter>(BaseQuery<TEntity, TFilter, TDbContext> definedQuery)
			where TEntity : class
			where TFilter : IQueryFilter;

		//Tranasction
		//Compinsate Transaction
		//Memento		
	}
}