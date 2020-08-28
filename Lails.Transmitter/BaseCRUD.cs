using Lails.Transmitter.DbCrud;
using Microsoft.EntityFrameworkCore;
using System;

namespace Lails.Transmitter
{
	internal class BaseCRUD<TDbContext>
		where TDbContext : DbContext
	{
		internal static IDbCRUD<TDbContext> DbCRUD
		{
			get
			{
				return (IDbCRUD<TDbContext>)_provider.GetService(typeof(IDbCRUD<TDbContext>));
			}
		}

		static IServiceProvider _provider = null;
		internal static void SetServiceProvider(IServiceProvider provider)
		{
			_provider = provider;
		}
	}
}
