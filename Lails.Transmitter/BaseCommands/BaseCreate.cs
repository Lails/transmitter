using Lails.Transmitter.CrudOperations;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Lails.Transmitter.BaseCommands
{
	public class BaseCreate<TDbContext, TData> : BaseCRUD<TDbContext>, IBaseOperation<TDbContext, TData>
		where TData : class
		where TDbContext : DbContext
	{
		public async Task Execute(TData data)
		{
			await BeforeCreateAsync(data);

			await _dbCRUD.CreateAsync(data);

			await AfterCreateAsync(data);
		}

		public virtual async Task BeforeCreateAsync(TData data) { }
		public virtual async Task AfterCreateAsync(TData data) { }
	}
}
