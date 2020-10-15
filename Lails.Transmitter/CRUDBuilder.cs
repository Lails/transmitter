using Lails.Transmitter.CrudOperations;
using Lails.Transmitter.DbCrud;
using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection;

namespace Lails.Transmitter
{
	public class CRUDBuilder<TDbContext> : ICRUDBuilder<TDbContext>
		where TDbContext : DbContext
	{
		readonly IDbCRUD<TDbContext> _dbCRUD;
		public CRUDBuilder(TDbContext dbContext)
		{
			_dbCRUD = new DbCRUD<TDbContext>(dbContext);
		}

		public IBaseCRUDOperation Build<IBaseCRUDOperation>()
			where IBaseCRUDOperation : BaseCRUD<TDbContext>, new()
		{
			IBaseCRUDOperation instance = Activator.CreateInstance<IBaseCRUDOperation>();
			instance.GetType().BaseType.BaseType.GetField(BaseCRUD<TDbContext>.DbCRUDFieldName, BindingFlags.NonPublic | BindingFlags.Instance).SetValue(instance, _dbCRUD);
			return instance;
		}
	}


	public interface ICRUDBuilder<TDbContext> where TDbContext : DbContext
	{
		IBaseOperation Build<IBaseOperation>() where IBaseOperation : BaseCRUD<TDbContext>, new();
	}
}
