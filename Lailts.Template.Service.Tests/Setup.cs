using Lails.DBContext;
using Lails.Transmitter;
using Lails.Transmitter.DbCrud;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace Lailts.Transmitter.Tests
{

	public class Setup
	{
		protected LailsDbContext Context;
		protected CRUDBuilder<LailsDbContext> CRUDBuilder;

		[OneTimeSetUp]
		public void SetUp()
		{
			var services = new ServiceCollection();


			services
				.AddEntityFrameworkInMemoryDatabase()
				.AddDbContext<LailsDbContext>((serviceProvider, options) => options.UseInMemoryDatabase("LailsDbContext").UseInternalServiceProvider(serviceProvider));

			services
				.AddDbCRUD<LailsDbContext>();

			var provider = services.BuildServiceProvider();


			Context = (LailsDbContext)provider.GetService(typeof(LailsDbContext));
			CRUDBuilder = (CRUDBuilder<LailsDbContext>)provider.GetService(typeof(CRUDBuilder<LailsDbContext>));

		}

		[OneTimeTearDown]
		public void TaerDown()
		{
		}


		public static CustomerStruct TestCustomer1 = new CustomerStruct { FirstName = "Angry", LastName = "Birdth", Address = "Sydnay" };
		public static CustomerStruct TestCustomer2 = new CustomerStruct { FirstName = "Red", LastName = "Birdth", Address = "Melbourne" };
		public struct CustomerStruct
		{
			public Guid Id { get; set; }
			public string FirstName { get; set; }
			public string LastName { get; set; }
			public string Address { get; set; }
		}
		public async Task SeedDatabase()
		{
			await Context.Customers.AddRangeAsync(new[] {
				new Customer { FirstName = TestCustomer1.FirstName, LastName = TestCustomer1.LastName, Address = TestCustomer1.Address },
				new Customer { FirstName = TestCustomer2.FirstName, LastName = TestCustomer2.LastName, Address = TestCustomer2.Address }
			});
			await Context.SaveChangesAsync();
		}
		public async Task ResetDatabase()
		{
			var invoces = await Context.Invoices.ToListAsync();
			Context.Invoices.RemoveRange(invoces);
			await Context.SaveChangesAsync();

			var customers = await Context.Customers.ToListAsync();
			Context.Customers.RemoveRange(customers);
			await Context.SaveChangesAsync();
		}
	}
}
