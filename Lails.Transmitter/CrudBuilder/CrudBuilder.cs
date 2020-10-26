using Lails.Transmitter.CrudOperations;
using Lails.Transmitter.DbCrud;
using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection;

namespace Lails.Transmitter.CrudBuilder
{
	public class CrudBuilder<TDbContext> : ICrudBuilder<TDbContext>
		where TDbContext : DbContext
	{
		readonly IDbCRUD<TDbContext> _dbCRUD;
		public CrudBuilder(TDbContext dbContext)
		{
			_dbCRUD = new DbCRUD<TDbContext>(dbContext);
		}

		public IBaseCRUDOperation Build<IBaseCRUDOperation>() where IBaseCRUDOperation : BaseCrudOperations<TDbContext>, new()
		{
			IBaseCRUDOperation instance = Activator.CreateInstance<IBaseCRUDOperation>();
			instance.GetType().BaseType.BaseType.GetField(BaseCrudOperations<TDbContext>.DbCRUDFieldName, BindingFlags.NonPublic | BindingFlags.Instance).SetValue(instance, _dbCRUD);
			return instance;
		}
	}
}