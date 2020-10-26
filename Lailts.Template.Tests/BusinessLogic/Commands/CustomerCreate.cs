using Lails.DBContext;
using Lails.Transmitter.BaseCommands;
using Lails.Transmitter.CrudOperations;
using System.Collections.Generic;

namespace Lailts.Transmitter.Tests.BusinessLogic.Commands
{
	public class CustomerCreate : BaseCreate<LailsDbContext, Customer>
	{
	}
	public class CustomersCreate : BaseCreate<LailsDbContext, List<Customer>>
	{
	}
}
