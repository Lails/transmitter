using Lails.Transmitter.Retriever;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Lails.Transmitter.DbCrud
{
	internal sealed class DbCRUD<TDbContext> : IDbCRUD<TDbContext> where TDbContext : DbContext
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

		public async Task CreateAsync<TData>(TData data) where TData : class
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

		public async Task UpdateAsync<TData>(TData data) where TData : class
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

		public async Task DeleteAsync<TData>(TData data) where TData : class
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

		public IRetriever<TEntity, TDbContext> Retriever<TEntity>() where TEntity : class
		{
			throw new NotImplementedException();
		}

		public IRetriever<TEntity, TDbContext> RetrieverAsNotTracking<TEntity>() where TEntity : class
		{
			throw new NotImplementedException();
		}
	}
}
