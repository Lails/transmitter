using Lails.Transmitter.CrudOperations;
using Lails.Transmitter.DbCrud;
using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection;

namespace Lails.Transmitter
{
	public class CRUDBuilder<TDbContext>
		where TDbContext : DbContext
	{
		readonly IDbCRUD<TDbContext> _dbCRUD;
		public CRUDBuilder(TDbContext dbContext)
		{
			_dbCRUD = new DbCRUD<TDbContext>(dbContext);
		}

		public IBaseOperation Build<IBaseOperation>()
		{
			IBaseOperation instance = Activator.CreateInstance<IBaseOperation>();
			instance.GetType().BaseType.BaseType.GetField(BaseCRUD<TDbContext>.DbCRUDFieldName, BindingFlags.NonPublic | BindingFlags.Instance).SetValue(instance, _dbCRUD);
			return instance;
		}
	}
}
