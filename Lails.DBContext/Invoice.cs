﻿using System;

namespace Lails.DBContext
{
	public class Invoice
	{
		public Guid Id { get; set; }
		public DateTime Date { get; set; }

		public Customer Customer { get; set; }
	}
}
