using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Threading.Tasks;

namespace Lails.Transmitter.Commander
{
	public class Command<TDbContext, TData> : ICommand<TData>
		where TData : class
		where TDbContext : DbContext
	{
		readonly TDbContext _context;
		public Command(TDbContext context)
		{
			_context = context;
		}


		public async Task CreateAsync(TData data)
		{

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

		public async Task UpdateAsync(TData data)
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

		public async Task DeleteAsync(TData data)
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
