using Lails.DBContext;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System;
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

			var customerResult = DbCRUD.Retriever<Customer>().GetById(customer.Id).Result;

			Assert.AreEqual(customer, customerResult);
		}

		[TestCase("Angry", 1)]
		[TestCase(null, 2)]
		[Test]
		public void Retriever_GetElementsByFilter_RetrunsElementsExpectedCount(string firstName, int expectedCount)
		{
			var filter = CustomerRetriver.Create()
				.SetFirstName(firstName);

			var customersResult = DbCRUD.Retriever<Customer>().GetByFilterAsync(filter).Result;

			Assert.AreEqual(expectedCount, customersResult.Count);
		}

		[Test]
		public void Retriever_GetAllElementsByFilterNull_RetrunsOneElement()
		{
			var customersResult = DbCRUD.Retriever<Customer>().GetByFilterAsync(null).Result;

			Assert.AreEqual(Context.Customers.Count(), customersResult.Count);
		}

		[Test]
		public void Retriever_GetAllElementsByAction_RetrunsOneElement()
		{
			var customersResult = DbCRUD.Retriever<Customer>().GetByAction(r => r.FirstName.ToUpper().Contains("angry".ToUpper())).Result;

			Assert.AreEqual(1, customersResult.Count);
		}

		[Test]
		public void Retriever_GetAllElementsByActionNull_RetrunsOneElement()
		{
			var customersResult = DbCRUD.Retriever<Customer>().GetByAction(null).Result;

			Assert.AreEqual(Context.Customers.Count(), customersResult.Count);
		}

		[Test]
		public void Retriever_CheckTracking_ReturnsSuccessChanges()
		{
			

			var newCustomer = new Customer { FirstName = MethodBase.GetCurrentMethod().Name };
			Context.Add(newCustomer);
			Context.SaveChanges();

			var customer = DbCRUD.Retriever<Customer>().GetById(newCustomer.Id).Result;
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

			var customer = DbCRUD.RetrieverAsNotTracking<Customer>().GetById(newCustomer.Id).Result;
			customer.FirstName += "_changed";
			Context.SaveChanges();

			var reloadedCustomer = Context.Customers.FirstOrDefault(r => r.Id == newCustomer.Id);

			Assert.AreEqual(reloadedCustomer.FirstName, MethodBase.GetCurrentMethod().Name);
		}
	}
}