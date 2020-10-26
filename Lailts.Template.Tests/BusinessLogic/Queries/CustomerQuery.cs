using Lails.DBContext;
using Lails.Transmitter.BaseQuery;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Lailts.Transmitter.Tests.BusinessLogic.Queries
{
	public class CustomerQuery : BaseQuery<Customer, CustomerFilter, LailsDbContext>
	{
		public override IQueryable<Customer> QueryDefinition(ref IQueryable<Customer> query)
		{
			return query
				.Include(r => r.Invoices);
		}

		public override IQueryable<Customer> QueryFilter(ref IQueryable<Customer> query, CustomerFilter filter)
		{
			if (filter.Id.HasValue)
			{
				query = query.Where(r => r.Id == filter.Id);
			}
			if (string.IsNullOrWhiteSpace(filter.FirstName) == false)
			{
				query = query.Where(r => r.FirstName == filter.FirstName);
			}

			return query;
		}
	}

	public class CustomerFilter : IQueryFilter
	{
		public static CustomerFilter Create() => new CustomerFilter();
		public Guid? Id { get; set; }
		public string FirstName { get; set; }

		public CustomerFilter SetId(Guid? id)
		{
			Id = id;
			return this;
		}
		public CustomerFilter SetfirstName(string firstName)
		{
			FirstName = firstName;
			return this;
		}
	}

}
