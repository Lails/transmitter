using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Lails.Transmitter.DbCrud
{
	public static class DbCRUDExtansion
	{
		public static IServiceCollection AddDbCRUD<TDbContext>(this IServiceCollection services) where TDbContext : DbContext
		{
			services
				.AddTransient<IDbCRUD<TDbContext>, DbCRUD<TDbContext>>()
				.AddTransient<CRUDBuilder<TDbContext>>();

			return services;
		}
	}
}
