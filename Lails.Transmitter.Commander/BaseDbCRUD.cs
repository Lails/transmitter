using Lails.Transmitter.Retriever;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Lails.Transmitter.Commander
{
	public abstract class BaseDbCRUD<TDbContext>
		where TDbContext : DbContext
	{
		internal abstract IRetriever<TEntity, TDbContext> Retriever<TEntity>() where TEntity : class;
		internal abstract IRetriever<TEntity, TDbContext> RetrieverAsNotTracking<TEntity>() where TEntity : class;


		internal abstract Task CreateAsync<TData>(TData data) where TData : class;
		internal abstract Task UpdateAsync<TData>(TData data) where TData : class;
		internal abstract Task DeleteAsync<TData>(TData data) where TData : class;

		//Tranasction
		//Compinsate Transaction
		//Memento
		//
	}
}