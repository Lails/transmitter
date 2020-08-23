using Lails.DBContext;
using Lails.Transmitter.Retriever;
using System.Linq;

namespace Lailts.Template.Service.Tests
{
	public class CustomerRetriver : BaseRetriver<Customer, LailsDbContext>
	{

		public static CustomerRetriver Create() => new CustomerRetriver();
		public override IQueryable<Customer> QueryDefinition()
		{
			var query = Query;

			if (FirstName?.Length > 0)
			{
				query = query.Where(r => r.FirstName == FirstName);
			}
			return query;
		}


		public string FirstName { get; private set; }
		public CustomerRetriver SetFirstName(string firstName)
		{
			FirstName = firstName;
			return this;
		}
	}
}