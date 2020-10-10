using Lails.Transmitter.DbCrud;
using Microsoft.EntityFrameworkCore;

namespace Lails.Transmitter.CrudOperations
{
	public abstract class BaseCRUD<TDbContext>
		where TDbContext : DbContext
	{
		internal IDbCRUD<TDbContext> _dbCRUD;

		internal static string DbCRUDFieldName => nameof(_dbCRUD);
	}
}
