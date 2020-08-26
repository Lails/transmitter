using Lails.Transmitter.Retriever;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Threading.Tasks;

namespace Lails.Transmitter.Commander
{
	//TODO: MAKE EXTANSION FOR RESOLVE THIS OBJECT
	//TODO: TO THINK MORE ABOUT BaseCommand<TDbContext>.SetDbCRUD(this);
	public sealed class DbCRUD<TDbContext> : BaseDbCRUD<TDbContext> where TDbContext : DbContext
	{
		readonly TDbContext _context;
		public DbCRUD(IServiceProvider provider)
		{
			_context = (TDbContext)provider.GetService(typeof(TDbContext));
			if (_context == null)
			{
				throw new NullReferenceException($"DbContext is null.");
			}
			BaseCommand<TDbContext>.SetDbCRUD(this);
		}

		/*

public ICommand<TEntity> For<TEntity>() where TEntity : class
{
	var command = new Command<TDbContext, TEntity>(_context);
	return command;
}*/


		internal override IRetriever<TEntity, TDbContext> Retriever<TEntity>()
		{
			return new Retriver<TEntity, TDbContext>(_context, false);
		}

		internal override IRetriever<TEntity, TDbContext> RetrieverAsNotTracking<TEntity>()
		{
			return new Retriver<TEntity, TDbContext>(_context, true);
		}



		internal override async Task CreateAsync<TData>(TData data)
		{
			if (data == null)
			{
				throw new ArgumentNullException(nameof(data));
			}

			if (data is IEnumerable enities)
			{
				foreach (var enity in enities)
				{
					await _context.AddAsync(enity);
				}
			}
			else
			{
				await _context.AddAsync(data);
			}
			await _context.SaveChangesAsync();
		}

		internal override async Task UpdateAsync<TData>(TData data)
		{
			if (data is IEnumerable enities)
			{
				foreach (var enity in enities)
				{
					_context.Update(enity);
				}
			}
			else
			{
				_context.Update(data);
			}
			await _context.SaveChangesAsync();
		}

		internal override async Task DeleteAsync<TData>(TData data)
		{
			if (data is IEnumerable enities)
			{
				foreach (var enity in enities)
				{
					_context.Remove(enity);
				}
			}
			else
			{
				_context.Remove(data);
			}
			await _context.SaveChangesAsync();
		}
	}
}
