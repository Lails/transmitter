using Lails.Transmitter.Retriever;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Lails.Transmitter.DbCrud
{
	internal interface IDbCRUD<TDbContext>
		where TDbContext : DbContext
	{
		IRetriever<TEntity, TDbContext> Retriever<TEntity>() where TEntity : class;
		IRetriever<TEntity, TDbContext> RetrieverAsNotTracking<TEntity>() where TEntity : class;


		Task CreateAsync<TData>(TData data) where TData : class;
		Task UpdateAsync<TData>(TData data) where TData : class;
		Task DeleteAsync<TData>(TData data) where TData : class;

		//Tranasction
		//Compinsate Transaction
		//Memento
		//
	}
}