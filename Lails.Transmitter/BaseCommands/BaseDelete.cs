using Lails.Transmitter.CrudOperations;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Lails.Transmitter.BaseCommands
{
	public abstract class BaseDelete<TData, TDbContext> : BaseCrudOperations<TDbContext>
		where TData : class
		where TDbContext : DbContext
	{
		public async Task Execute(TData data)
		{
			await BeforeDeleteAsync(data);

			await _dbCRUD.DeleteAsync(data);

			await AfterDeleteAsync(data);
		}
		protected virtual async Task BeforeDeleteAsync(TData data) { }
		protected virtual async Task AfterDeleteAsync(TData data) { }
	}
}