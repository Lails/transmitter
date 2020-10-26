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
	await _crudBuilder.Build<CustomerCreate>().Execute(customer);	
 	///Update
	await _crudBuilder.Build<CustomerUpdate>().Execute(customer);
	///Delete
	await _crudBuilder.Build<CustomerDelete>().Execute(customer);
	
	
	//Example createClass class:	
	public class CustomerCreate : BaseCreate<YOURDbContext, Customer> {}
	public class CustomersCreate : BaseCreate<YOURDbContext, List<Customer>> {}
	
	
``` 

#### Read operations:
```C#
 
	//filter example:	
	CustomerFilter filter = new CustomerFilter { Id = customer.Id };
	var customers = await _crudBuilder.Build<CustomerQuery>().ApplyFilter(filter);
	//Here the variable "customers" every time will be List<Customer>.
	
	//Filter example.
	//public class CustomerQuery : BaseQuery<Customer, CustomerFilter, YOURDbContext>
	//{
	//	public override IQueryable<Customer> QueryDefinition(ref IQueryable<Customer> query)
	//	{
	//		return query
	//			.Include(r=>r.Invoices); 
	//	}
	//
	//	public override IQueryable<Customer> QueryFilter(ref IQueryable<Customer> query, CustomerFilter filter)
	//	{
	//		if (filter.Id.HasValue)
	//		{
	//			query = query.Where(r => r.Id == filter.Id);
	//		}
	//
	//		return query;
	//	}
	//}

	//public class CustomerFilter : IQueryFilter
	//{
	//	public Guid? Id { get; set; }
	//}
``` 
