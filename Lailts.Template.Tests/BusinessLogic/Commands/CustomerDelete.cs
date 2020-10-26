using Lails.DBContext;
using Lails.Transmitter.BaseCommands;

namespace Lailts.Transmitter.Tests.BusinessLogic.Commands
{
	class CustomerDelete : BaseDelete<Customer, LailsDbContext> { }
	class CustomersDelete : BaseDelete<Customer[], LailsDbContext> { }
}
