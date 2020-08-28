using Lails.Transmitter.DbCrud;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Lails.Transmitter.BaseCommands
{
	public class BaseCreate<TData, TDbContext>
		where TData : class
		where TDbContext : DbContext
	{
		private IDbCRUD<TDbContext> DbCRUD => BaseCRUD<TDbContext>.DbCRUD;

		public static async Task CreateAsync(TData data)
		{
			//we can add service provider here if need;
			var baseCreate = new BaseCreate<TData, TDbContext>();

			await baseCreate.BeforeCreateAsync(data);

			await baseCreate.DbCRUD.CreateAsync(data);

			await baseCreate.AfterCreateAsync(data);
		}


		public virtual async Task BeforeCreateAsync(TData data) { }
		public virtual async Task AfterCreateAsync(TData data) { }
	}
}
