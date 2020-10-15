using Lails.DBContext;
using Lails.Template.Load.Tetst.BusinessLogic.Commands;
using Lails.Transmitter;
using MassTransit;
using System;
using System.Threading.Tasks;

namespace Lails.Template.Load.Tetst.Consumers
{
	public class LoadTestConsumer : IConsumer<ILoadTestEvent>
	{
		readonly ICRUDBuilder<LailsDbContext> _cRUDBuilder;
		public LoadTestConsumer(ICRUDBuilder<LailsDbContext> cRUDBuilder)
		{
			_cRUDBuilder = cRUDBuilder;
		}
		public async Task Consume(ConsumeContext<ILoadTestEvent> context)
		{
			var customer = new Customer { FirstName = "Elizabeth", LastName = "Lincoln", Address = "23 Tsawassen Blvd." };

			await _cRUDBuilder.Build<CustomerCreate>().Execute(customer);

			Console.WriteLine(customer.Id);
		}
	}

	public interface ILoadTestEvent
	{

	}
}
