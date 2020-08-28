using Lails.Transmitter.DbCrud;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Lails.Transmitter.BaseCommands
{
	public class BaseUpdate<TData, TDbContext>
		where TData : class
		where TDbContext : DbContext
	{
		internal static IDbCRUD<TDbContext> DbCRUD => BaseCRUD<TDbContext>.DbCRUD;

		public static async Task UpdateAsync(TData data)
		{
			//we can add service provider here if need;
			var baseUpdate = new BaseUpdate<TData, TDbContext>();

			await baseUpdate.BeforeUpdateAsync(data);

			await DbCRUD.UpdateAsync(data);

			await baseUpdate.AfterUpdateAsync(data);

			await baseUpdate.ChangeTrackerAsync();
		}

		protected virtual async Task BeforeUpdateAsync(TData data) { }
		protected virtual async Task AfterUpdateAsync(TData data) { }
		protected virtual async Task ChangeTrackerAsync(/*TODO:*/) { }
	}
}
