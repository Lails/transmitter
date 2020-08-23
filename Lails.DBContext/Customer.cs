using System;
using System.Collections.Generic;

namespace Lails.DBContext
{
	public class Customer
	{
		public Guid Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Address { get; set; }
		public virtual List<Invoice> Invoices { get; set; }
	}
}
