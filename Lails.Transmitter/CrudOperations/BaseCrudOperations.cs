using Lails.Transmitter.DbCrud;
using Microsoft.EntityFrameworkCore;

namespace Lails.Transmitter.CrudOperations
{
	public abstract class BaseCrudOperations<TDbContext>
		where TDbContext : DbContext
	{
		internal BaseCrudOperations() { }

		internal IDbCRUD<TDbContext> _dbCRUD;
		internal static string DbCRUDFieldName => nameof(_dbCRUD);
	}
}
