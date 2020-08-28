using Lails.DBContext;
using Lails.Transmitter.BaseCommands;

namespace Lailts.Template.Service.Tests.BusinessLogic.Commands
{
	public class CustomerUpdate : BaseUpdate<Customer, LailsDbContext> { }
	public class CustomersUpdate : BaseUpdate<Customer[], LailsDbContext> { }
}
