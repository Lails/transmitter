using Lails.DBContext;
using Lailts.Transmitter.Tests;
using Lailts.Transmitter.Tests.BusinessLogic.Queries;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Lailts.Transmitter.Service.Tests
{
	public class DbCrudRetriverTests : Setup
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
		public void Retriever_GetElementById_RetrunsOneElement()
		{
			var customer = new Customer
			{
				Id = Guid.NewGuid(),
				FirstName = "Sarah",
				LastName = "Conor",
				Address = "Sydney",
				Invoices = new List<Invoice> { new Invoice { Date = DateTime.UtcNow } }
			};
			Context.Customers.Add(customer);
			Context.SaveChanges();
			var filter = CustomerFilter.Create()
				.SetId(customer.Id);

			List<Customer> customersResult = CRUDBuilder.Build<CustomerQuery>().ApplyFilter(filter).Result;

			Assert.AreEqual(customer, customersResult.Single());
		}

		[TestCase("Angry", 1)]
		[TestCase(null, 2)]
		[Test]
		public void Retriever_GetElementsByFilter_RetrunsElementsExpectedCount(string firstName, int expectedCount)
		{
			var filter = CustomerFilter.Create()
				.SetfirstName(firstName);

			List<Customer> customersResult = CRUDBuilder.Build<CustomerQuery>().ApplyFilter(filter).Result;

			Assert.AreEqual(expectedCount, customersResult.Count);
		}

		[Test]
		public void Retriever_GetAllElementsByFilterNull_RetrunsOneElement()
		{
			List<Customer> customersResult = CRUDBuilder.Build<CustomerQuery>().ApplyFilter(null).Result;

			Assert.AreEqual(Context.Customers.Count(), customersResult.Count);
		}

		[Test]
		public void Retriever_CheckTracking_ReturnsSuccessChanges()
		{
			var newCustomer = new Customer { FirstName = MethodBase.GetCurrentMethod().Name };
			Context.Add(newCustomer);
			Context.SaveChanges();

			var filter = CustomerFilter.Create()
				.SetId(newCustomer.Id);
			Customer customer = CRUDBuilder.Build<CustomerQuery>().ApplyFilter(filter).Result.Single();
			customer.FirstName += "_changed";
			Context.SaveChanges();

			var reloadedCustomer = Context.Customers.FirstOrDefault(r => r.Id == newCustomer.Id);
			Assert.AreEqual(reloadedCustomer.FirstName, customer.FirstName);
		}
		[Test]
		public void RetrieverAsNoTracking_CheckNoTracking_ReturnsNoChanges()
		{
			var newCustomer = new Customer { FirstName = MethodBase.GetCurrentMethod().Name };
			Context.Add(newCustomer);
			Context.SaveChanges();

			var filter = CustomerFilter.Create()
				.SetId(newCustomer.Id);

			Customer customer = CRUDBuilder.Build<CustomerQuery>().ApplyFilterAsNoTraking(filter).Result.Single();
			customer.FirstName += "_changed";
			Context.SaveChanges();

			var reloadedCustomer = Context.Customers.FirstOrDefault(r => r.Id == newCustomer.Id);
			Assert.AreEqual(reloadedCustomer.FirstName, MethodBase.GetCurrentMethod().Name);
		}
		//TODO: Add test, what will write AsNotraking in QueryDefinition
	}
}