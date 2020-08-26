using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Lails.Transmitter.Commander
{
	public class BaseDelete<TData, TDbContext> : BaseCommand<TDbContext>
		where TData : class
		where TDbContext : DbContext
	{
		public static async Task UpdateAsync(TData data)
		{
			//we can add service provider here if need;
			var baseUpdate = new BaseDelete<TData, TDbContext>();

			await baseUpdate.BeforeDeleteAsync(data);

			await DbCRUD.UpdateAsync(data);

			await baseUpdate.AfterDeleteAsync(data);
		}

		protected virtual async Task BeforeDeleteAsync(TData data) { }
		protected virtual async Task AfterDeleteAsync(TData data) { }
	}
}
