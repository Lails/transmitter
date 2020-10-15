using Lails.Transmitter.CrudOperations;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Lails.Transmitter.BaseCommands
{
	public abstract class BaseUpdate<TData, TDbContext> : BaseCRUD<TDbContext>
		where TData : class
		where TDbContext : DbContext
	{
		public async Task Execute(TData data)
		{
			await BeforeUpdateAsync(data);

			await _dbCRUD.UpdateAsync(data);

			await AfterUpdateAsync(data);
		}

		protected virtual async Task BeforeUpdateAsync(TData data) { }
		protected virtual async Task AfterUpdateAsync(TData data) { }
		protected virtual async Task ChangeTrackerAsync(/*TODO:*/) { }
	}
}