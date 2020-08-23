using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Lails.Transmitter.Retriever
{
	public interface IBaseRetriver<TEntity>
		where TEntity : class
	{
		IQueryable<TEntity> QueryDefinition();
	}
}
