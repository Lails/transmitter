using Lails.Transmitter.DbCrud;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Lails.Transmitter.BaseCommands
{
	public class BaseDelete<TData, TDbContext>
		where TData : class
		where TDbContext : DbContext
	{
		internal IDbCRUD<TDbContext> DbCRUD => BaseCRUD<TDbContext>.DbCRUD;

		public static async Task DeleteAsync(TData data)
		{
			//we can add service provider here if need;
			var baseDelete = new BaseDelete<TData, TDbContext>();

			await baseDelete.BeforeDeleteAsync(data);

			await baseDelete.DbCRUD.DeleteAsync(data);

			await baseDelete.AfterDeleteAsync(data);
		}

		protected virtual async Task BeforeDeleteAsync(TData data) { }
		protected virtual async Task AfterDeleteAsync(TData data) { }
	}
}
