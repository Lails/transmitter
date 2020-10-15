using Lails.Transmitter.CrudOperations;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Lails.Transmitter.BaseQuery
{
	public abstract class BaseQuery<TData, TDbContext> : BaseCRUD<TDbContext>
		where TData : class
		where TDbContext : DbContext
	{
		public Task Execute(TData data)
		{
			throw new System.NotImplementedException();
		}
	}
}