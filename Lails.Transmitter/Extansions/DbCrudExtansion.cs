using Lails.Transmitter.CrudBuilder;
using Lails.Transmitter.DbCrud;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Lails.Transmitter.Extansions
{
	public static class DbCrudExtansion
	{
		public static IServiceCollection AddDbCrud<TDbContext>(this IServiceCollection services) where TDbContext : DbContext
		{
			services
				.AddTransient<IDbCRUD<TDbContext>, DbCRUD<TDbContext>>()
				.AddTransient<ICrudBuilder<TDbContext>, CrudBuilder<TDbContext>>();

			return services;
		}
	}
}
