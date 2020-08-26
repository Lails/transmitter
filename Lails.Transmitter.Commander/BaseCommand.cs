using Microsoft.EntityFrameworkCore;

namespace Lails.Transmitter.Commander
{
	public class BaseCommand<TDbContext>
		where TDbContext : DbContext
	{
		protected static BaseDbCRUD<TDbContext> DbCRUD { get; set; }
		internal static void SetDbCRUD(BaseDbCRUD<TDbContext> dbCRUD)
		{
			DbCRUD = dbCRUD;
		}
	}
}
