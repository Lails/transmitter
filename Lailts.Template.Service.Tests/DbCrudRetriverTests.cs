using Lails.DBContext;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Lailts.Template.Service.Tests
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
			var customer = new Customer { Id = Guid.NewGuid(), FirstName = "Sarah", LastName = "Conor", Address = "Sydnay" };
			Context.Customers.Add(customer);
			Context.SaveChanges();

			Customer customerResult = null;// DbCRUD.Retriever<Customer>().GetById(customer.Id).Result;

			Assert.AreEqual(customer, customerResult);
		}

		[TestCase("Angry", 1)]
		[TestCase(null, 2)]
		[Test]
		public void Retriever_GetElementsByFilter_RetrunsElementsExpectedCount(string firstName, int expectedCount)
		{
			var filter = CustomerRetriver.Create()
				.SetFirstName(firstName);

			List<Customer> customersResult = null;//  DbCRUD.Retriever<Customer>().GetByFilterAsync(filter).Result;

			Assert.AreEqual(expectedCount, customersResult.Count);
		}

		[Test]
		public void Retriever_GetAllElementsByFilterNull_RetrunsOneElement()
		{
			List<Customer> customersResult = null;//  DbCRUD.Retriever<Customer>().GetByFilterAsync(null).Result;

			Assert.AreEqual(Context.Customers.Count(), customersResult.Count);
		}

		[Test]
		public void Retriever_GetAllElementsByAction_RetrunsOneElement()
		{
			List<Customer> customersResult = null;// DbCRUD.Retriever<Customer>().GetByAction(r => r.FirstName.ToUpper().Contains("angry".ToUpper())).Result;

			Assert.AreEqual(1, customersResult.Count);
		}

		[Test]
		public void Retriever_GetAllElementsByActionNull_RetrunsOneElement()
		{
			List<Customer> customersResult = null;//  DbCRUD.Retriever<Customer>().GetByAction(null).Result;

			Assert.AreEqual(Context.Customers.Count(), customersResult.Count);
		}

		[Test]
		public void Retriever_CheckTracking_ReturnsSuccessChanges()
		{
			var newCustomer = new Customer { FirstName = MethodBase.GetCurrentMethod().Name };
			Context.Add(newCustomer);
			Context.SaveChanges();

			Customer customer = null;//  DbCRUD.Retriever<Customer>().GetById(newCustomer.Id).Result;
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

			Customer customer = null;//  DbCRUD.RetrieverAsNotTracking<Customer>().GetById(newCustomer.Id).Result;
			customer.FirstName += "_changed";
			Context.SaveChanges();

			var reloadedCustomer = Context.Customers.FirstOrDefault(r => r.Id == newCustomer.Id);

			Assert.AreEqual(reloadedCustomer.FirstName, MethodBase.GetCurrentMethod().Name);
		}
		//TODO: Add test, what will write AsNotraking in QueryDefinition
	}
}