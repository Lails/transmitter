using Lails.DBContext;
using Lails.Transmitter.BaseCommands;
using System.Collections.Generic;

namespace Lails.Template.Load.Tetst.BusinessLogic.Commands
{
	public class CustomerCreate : BaseCreate<LailsDbContext, Customer>
	{
	}
	public class CustomersCreate : BaseCreate<LailsDbContext, List<Customer>>
	{
	}
}
