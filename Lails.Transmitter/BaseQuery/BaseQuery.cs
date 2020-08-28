using Microsoft.EntityFrameworkCore;

namespace Lails.Transmitter.BaseQuery
{
	public abstract class BaseQuery<TData, TDbContext>
		where TData : class
		where TDbContext : DbContext
	{
	}
}
