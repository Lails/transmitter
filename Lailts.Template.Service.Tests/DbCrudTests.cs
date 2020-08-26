using Lails.DBContext;
using Lails.Transmitter.Commander;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System;
using System.Linq;
using System.Reflection;

namespace Lailts.Template.Service.Tests
{
	public class DbCrudTests : Setup
	{
		[SetUp]
		public void Setup()
		{
			SeedDatabase().Wait();
		}
		[TearDown]
		public void Down()
		{
			ResetDatabase().Wait();
		}

		public class CreateCustomer : BaseCreate<Customer, LailsDbContext> { }
		public class CreateCustomers : BaseCreate<Customer[], LailsDbContext> { }


		[Test]
		public void CreateAsync_CreateElement_Success()
		{
			var customer = new Customer { FirstName = "Elizabeth", LastName = "Lincoln", Address = "23 Tsawassen Blvd.", };
			
			CreateCustomer.CreateAsync(customer).Wait();
			

			var existingCustomer = Context.Customers.FirstOrDefault(r => r.FirstName == "Elizabeth");
			Assert.NotNull(existingCustomer);
		}

		[Test]
		public void CreateRangeAsync_CreateRangeElements_Success()
		{
			var countsBeforeCreate = Context.Customers.Count();
			var customer1 = new Customer { FirstName = "CreateRangeAsync", LastName = "Lincoln", Address = "23 Tsawassen Blvd.", };
			var customer2 = new Customer { FirstName = "CreateRangeAsync", LastName = "Lincoln", Address = "23 Tsawassen Blvd.", };
			var customers = new[] { customer1, customer2 };

			CreateCustomers.CreateAsync(customers).Wait();


			Assert.AreEqual(countsBeforeCreate + customers.Length, Context.Customers.Count());
		}
		[Test]
		public void CreateRangeAsync_PassNUll_ThrowNullArgumentException()
		{
			var ex = Assert.Throws<AggregateException>(() =>
			{
				CreateCustomer.CreateAsync(null).Wait();
			});

			Assert.AreEqual(ex.InnerException.GetType(), typeof(ArgumentNullException));
		}

		[Test]
		public void UpdateAsync_UpdateElement_MutchChangedValue()
		{
			var customer = Context.Customers.Single(r => r.FirstName == TestCustomer1.FirstName);
			var newFirstName = MethodBase.GetCurrentMethod().Name;

			customer.FirstName = newFirstName;
			//DbCRUD.UpdateAsync(DbCRUD, customer);


			var changedCustomer = Context.Customers.Single(r => r.FirstName == newFirstName);
			Assert.AreEqual(changedCustomer.FirstName, newFirstName);
		}

		[Test]
		public void UpdateAsync_UpdateRangeElements_Success()
		{
			var customer = Context.Customers.Single(r => r.FirstName == TestCustomer1.FirstName);
			var customer2 = Context.Customers.Single(r => r.FirstName == TestCustomer2.FirstName);
			var newFirstName = MethodBase.GetCurrentMethod().Name;

			customer.FirstName = newFirstName;
			customer2.FirstName = newFirstName;
			//DbCRUD.UpdateAsync(DbCRUD, new[] { customer, customer2 });

			var customers = Context.Customers.Where(r => r.FirstName == newFirstName).ToList();
			Assert.AreEqual(customers.Count, 2);
		}

		[Test]
		public void DeleteAsync_DeleteElement_Success()
		{
			var customer = Context.Customers.Single(r => r.FirstName == TestCustomer1.FirstName);

			//DbCRUD.DeleteAsync(DbCRUD, customer);

			var deletedCustomer = Context.Customers.FirstOrDefault(r => r.FirstName == TestCustomer1.FirstName);
			Assert.IsNull(deletedCustomer);
		}

		[Test]
		public void DeleteAsync_DeleteRangeElements_Success()
		{
			var customer = Context.Customers.Single(r => r.FirstName == TestCustomer1.FirstName);
			var customer2 = Context.Customers.Single(r => r.FirstName == TestCustomer2.FirstName);

			//DbCRUD.For<Customer[]>().DeleteAsync(new[] { customer, customer2 });

			var customers = Context.Customers.Where(r => r.FirstName == TestCustomer1.FirstName || r.FirstName == TestCustomer2.FirstName).ToList();
			Assert.AreEqual(customers.Count, 0);
		}
	}
}