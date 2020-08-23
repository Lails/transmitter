using Lails.Transmitter.Retriever;
using Microsoft.EntityFrameworkCore;

namespace Lails.Transmitter.Commander
{
	public interface IDbCRUD<TDbContext> where TDbContext : DbContext
	{
		ICommand<TData> For<TData>() where TData : class;

		IRetriever<TEntity, TDbContext> Retriever<TEntity>() where TEntity : class;
		IRetriever<TEntity, TDbContext> RetrieverAsNotTracking<TEntity>() where TEntity : class;


		//Tranasction
		//Compinsate Transaction
		//Memento
		//
	}
}