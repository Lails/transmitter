using Lails.DBContext;
using Lailts.Template.Service.Tests.BusinessLogic.Commands;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System;
using System.Collections.Generic;
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

		[Test]
		public void CreateAsync_CreateElement_Success()
		{
			var customer = new Customer { FirstName = "Elizabeth", LastName = "Lincoln", Address = "23 Tsawassen Blvd.", };

			CustomerCreate.CreateAsync(customer).Wait();

			var existingCustomer = Context.Customers.Single(r => r.Id == customer.Id);
			Assert.AreEqual(existingCustomer?.FirstName, customer.FirstName);
		}

		[Ignore("load test")]
		[TestCase(10000)]
		[TestCase(1000)]
		[TestCase(100)]
		[Test]
		public void CreateAsync_CreateManyElements_Success(int customerCount)
		{
			var customers = new List<Customer>();
			for (int i = 0; i < customerCount; i++)
			{
				customers.Add(new Customer { FirstName = "Elizabeth", LastName = "Lincoln", Address = "23 Tsawassen Blvd." });

			}
			CustomersCreate.CreateAsync(customers).Wait();

			var existingCustomers = Context.Customers.Where(r => r.FirstName == "Elizabeth").ToList();
			Assert.AreEqual(customerCount, existingCustomers?.Count);
		}

		[Test]
		public void CreateRangeAsync_CreateRangeElements_Success()
		{
			var countsBeforeCreate = Context.Customers.Count();
			var customer1 = new Customer { FirstName = "CreateRangeAsync", LastName = "Lincoln", Address = "23 Tsawassen Blvd.", };
			var customer2 = new Customer { FirstName = "CreateRangeAsync", LastName = "Lincoln", Address = "23 Tsawassen Blvd.", };
			var customers = new[] { customer1, customer2 };

			CustomersCreate.CreateAsync(customers.ToList()).Wait();

			Assert.AreEqual(countsBeforeCreate + customers.Length, Context.Customers.Count());
		}
		[Test]
		public void CreateRangeAsync_PassNUll_ThrowNullArgumentException()
		{
			Customer customer = null;

			var ex = Assert.Throws<AggregateException>(() => { CustomerCreate.CreateAsync(customer).Wait(); });

			Assert.AreEqual(ex.InnerException.GetType(), typeof(ArgumentNullException));
		}

		[Test]
		public void UpdateAsync_UpdateElement_MutchChangedValue()
		{
			var customer = Context.Customers.Single(r => r.FirstName == TestCustomer1.FirstName);
			var newFirstName = MethodBase.GetCurrentMethod().Name;

			customer.FirstName = newFirstName;
			CustomerUpdate.UpdateAsync(customer).Wait();

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
			CustomersUpdate.UpdateAsync(new[] { customer, customer2 }).Wait();

			var customers = Context.Customers.Where(r => r.FirstName == newFirstName).ToList();
			Assert.AreEqual(customers.Count, 2);
		}

		[Test]
		public void DeleteAsync_DeleteElement_Success()
		{
			var customer = Context.Customers.Single(r => r.FirstName == TestCustomer1.FirstName);

			CustomerDelete.DeleteAsync(customer).Wait();

			var deletedCustomer = Context.Customers.FirstOrDefault(r => r.FirstName == TestCustomer1.FirstName);
			Assert.IsNull(deletedCustomer);
		}

		[Test]
		public void DeleteAsync_DeleteRangeElements_Success()
		{
			var customer = Context.Customers.Single(r => r.FirstName == TestCustomer1.FirstName);
			var customer2 = Context.Customers.Single(r => r.FirstName == TestCustomer2.FirstName);

			CustomersDelete.DeleteAsync(new[] { customer, customer2 }).Wait();

			var customers = Context.Customers.Where(r => r.FirstName == TestCustomer1.FirstName || r.FirstName == TestCustomer2.FirstName).ToList();
			Assert.AreEqual(customers.Count, 0);
		}
	}
}