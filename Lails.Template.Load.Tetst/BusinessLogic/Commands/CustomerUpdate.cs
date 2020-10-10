using Lails.DBContext;
using Lails.Transmitter.BaseCommands;

namespace Lails.Template.Load.Tetst.BusinessLogic.Commands
{
	public class CustomerUpdate : BaseUpdate<Customer, LailsDbContext> { }
	public class CustomersUpdate : BaseUpdate<Customer[], LailsDbContext> { }
}
