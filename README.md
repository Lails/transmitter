# Transmitter

## USAGE:

#### Add CRUD into IServiceCollection: 
```C#
	Services.AddDbCrud<YOURDbContext>();
```

#### Create,Update,Delete operations:
```C#
	//Use DI for using builder.
	ICrudBuilder<YOURDbContext> cRUDBuilder

	//Create
	await _cRUDBuilder.Build<CustomerCreate>().Execute(customer);	
 	///Update
	await _cRUDBuilder.Build<CustomerUpdate>().Execute(customer);
	///Delete
	await _cRUDBuilder.Build<CustomerDelete>().Execute(customer);
	
	
	//Example createClass class:	
	public class CustomerCreate : BaseCreate<YOURDbContext, Customer> {}
	public class CustomersCreate : BaseCreate<YOURDbContext, List<Customer>> {}
	
	
``` 

#### Read operations:
```C#
 
	//filter example:	
	CustomerFilter filter = new CustomerFilter { Id = customer.Id };
	var r = await _cRUDBuilder.Build<CustomerQuery>().ApplyFilter(filter);
	
	
	//Filter example.
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
