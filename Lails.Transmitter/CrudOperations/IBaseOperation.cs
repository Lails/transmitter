using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Lails.Transmitter.CrudOperations
{
	public interface IBaseOperation<TDbContext, TData>
		where TData : class
		where TDbContext : DbContext
	{
		Task Execute(TData data);
	}
}
