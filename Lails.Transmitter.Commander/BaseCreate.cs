using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Lails.Transmitter.Commander
{
	public class BaseCreate<TData, TDbContext> : BaseCommand<TDbContext>
		where TData : class
		where TDbContext : DbContext
	{
		public static async Task CreateAsync(TData data)
		{
			//we can add service provider here if need;
			var baseCreate = new BaseCreate<TData, TDbContext>();

			await baseCreate.BeforeCreateAsync(data);

			await DbCRUD.CreateAsync(data);

			await baseCreate.AfterCreateAsync(data);
		}

		protected virtual async Task BeforeCreateAsync(TData data) { }
		protected virtual async Task AfterCreateAsync(TData data) { }
	}
}
