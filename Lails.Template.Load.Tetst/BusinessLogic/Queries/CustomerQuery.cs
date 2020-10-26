﻿using Lails.DBContext;
using Lails.Transmitter.BaseQuery;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Lails.Template.Load.Tetst.BusinessLogic.Queries
{
	public class CustomerQuery : BaseQuery<Customer, CustomerFilter, LailsDbContext>
	{
		public override IQueryable<Customer> QueryDefinition(ref IQueryable<Customer> query)
		{
			return query; 
		}

		public override IQueryable<Customer> QueryFilter(ref IQueryable<Customer> query, CustomerFilter filter)
		{
			if (filter.Id.HasValue)
			{
				query = query.Where(r => r.Id == filter.Id);
			}

			return query;
		}
	}

	public class CustomerFilter : IQueryFilter
	{
		public Guid? Id { get; set; }
	}

}
