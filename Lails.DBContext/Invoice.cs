using System;

namespace Lails.DBContext
{
	public class Invoice
	{
		public Guid Id { get; set; }
		public DateTime Date { get; set; }

		public int CustomerId { get; set; }
		public virtual Customer Customer { get; set; }
	}
}
