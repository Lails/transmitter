using Lails.DBContext;
using Lails.Transmitter.BaseCommands;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lailts.Template.Service.Tests.BusinessLogic.Commands
{
	public class CustomerCreate : BaseCreate<Customer, LailsDbContext>
	{ 
	}
	public class CustomersCreate : BaseCreate<List<Customer>, LailsDbContext>
	{
	}
}
