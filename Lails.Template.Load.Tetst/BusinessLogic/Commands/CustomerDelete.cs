using Lails.DBContext;
using Lails.Transmitter.BaseCommands;

namespace Lails.Template.Load.Tetst.BusinessLogic.Commands
{
	class CustomerDelete : BaseDelete<Customer, LailsDbContext> { }
	class CustomersDelete : BaseDelete<Customer[], LailsDbContext> { }
}
