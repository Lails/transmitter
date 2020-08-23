using Lails.Transmitter.Retriever;
using Microsoft.EntityFrameworkCore;
using System;

namespace Lails.Transmitter.Commander
{
	public sealed class DbCRUD<TDbContext> : IDbCRUD<TDbContext> where TDbContext : DbContext
	{
		readonly TDbContext _context;
		public DbCRUD(IServiceProvider provider)
		{
			_context = (TDbContext)provider.GetService(typeof(TDbContext));
			if (_context == null)
			{
				throw new NullReferenceException($"DbContext is null.");
			}
		}

		public ICommand<TEntity> For<TEntity>() where TEntity : class
		{
			var command = new Command<TDbContext, TEntity>(_context);
			return command;
		}


		public IRetriever<TEntity, TDbContext> Retriever<TEntity>() where TEntity : class
		{
			return new Retriver<TEntity, TDbContext>(_context, false);
		}

		public IRetriever<TEntity, TDbContext> RetrieverAsNotTracking<TEntity>() where TEntity : class
		{
			return new Retriver<TEntity, TDbContext>(_context, true);
		}
	}
}
