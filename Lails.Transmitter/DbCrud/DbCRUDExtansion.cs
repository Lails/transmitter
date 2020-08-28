using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Lails.Transmitter.DbCrud
{
	public static class DbCRUDExtansion
	{
		public static IServiceCollection AddDbCRUD<TDbContext>(this IServiceCollection services) where TDbContext : DbContext
		{
			services
				.AddTransient<IDbCRUD<TDbContext>, DbCRUD<TDbContext>>();

			return services;
		}

		public static IServiceProvider AddDbCRUD<TDbContext>(this IServiceProvider provider) where TDbContext : DbContext
		{
			BaseCRUD<TDbContext>.SetServiceProvider(provider);

			return provider;
		}
	}
}
