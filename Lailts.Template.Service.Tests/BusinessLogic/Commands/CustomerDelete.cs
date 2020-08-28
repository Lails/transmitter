using Lails.DBContext;
using Lails.Transmitter.BaseCommands;

namespace Lailts.Template.Service.Tests.BusinessLogic.Commands
{
	class CustomerDelete : BaseDelete<Customer, LailsDbContext> { }
	class CustomersDelete : BaseDelete<Customer[], LailsDbContext> { }
}
