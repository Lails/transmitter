using Lails.Transmitter.CrudOperations;
using Microsoft.EntityFrameworkCore;

namespace Lails.Transmitter.CrudBuilder
{
	public interface ICrudBuilder<TDbContext> where TDbContext : DbContext
	{
		IBaseCRUDOperation Build<IBaseCRUDOperation>() where IBaseCRUDOperation : BaseCrudOperations<TDbContext>, new();
	}
}
