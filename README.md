# Transmitter

## USAGE:

#### Add CRUD into IServiceCollection: 
```C#
	Services.AddDbCrud<YOURDbContext>();
```

#### Create,Update,Delete operations:
```C#
	use inject for using builder.
	ICrudBuilder<YOURDbContext> cRUDBuilder

	//Create
	await _cRUDBuilder.Build<CustomerCreate>().Execute(customer);
	
		example CusomterCreate:	
		public class CustomerCreate : BaseCreate<YOURDbContext, Customer> {}
		public class CustomersCreate : BaseCreate<YOURDbContext, List<Customer>> {}
	
 	///Update
	await _cRUDBuilder.Build<CustomerUpdate>().Execute(customer);
	///Delete
	await _cRUDBuilder.Build<CustomerDelete>().Execute(customer);
	
	
``` 

#### Read operations:
```C#
 
	//filter example:	
	CustomerFilter filter = new CustomerFilter { Id = customer.Id };
	var r = await _cRUDBuilder.Build<CustomerQuery>().ApplyFilter(filter);
	
	
	Here is class example for making filter
	

	//public class CustomerQuery : BaseQuery<Customer, CustomerFilter, YOURDbContext>
	//{
	//	public override IQueryable<Customer> QueryDefinition(ref IQueryable<Customer> query)
	//	{
	//		return query; 
	//	}

	//	public override IQueryable<Customer> QueryFilter(ref IQueryable<Customer> query, CustomerFilter filter)
	//	{
	//		if (filter.Id.HasValue)
	//		{
	//			query = query.Where(r => r.Id == filter.Id);
	//		}

	//		return query;
	//	}
	//}

	//public class CustomerFilter : IQueryFilter
	//{
	//	public Guid? Id { get; set; }
	//}
``` 
