# Transmitter

## USAGE:

#### Add CRUD into IServiceCollection: 
```C#
	Services.AddTransient<IDbCRUD<YOURDbContext>, DbCRUD<YOURDbContext>>();
```

#### Create,Update,Delete operations:
```C#
	//Create
	DbCRUD.For<Customer>().CreateAsync(customer).Wait();
 	DbCRUD.For<Customer[]>().CreateAsync(customers).Wait();
 	///Update
	DbCRUD.For<Customer>().UpdateAsync(customer).Wait();
 	DbCRUD.For<Customer[]>().UpdateAsync(customers).Wait();
	///Delete
	DbCRUD.For<Customer>().DeleteAsync(customer).Wait();
 	DbCRUD.For<Customer[]>().DeleteAsync(customers).Wait();
``` 

#### Read operations:
```C#
	//GetById
	var customerResult = DbCRUD.Retriever<Customer>().GetById(customer.Id).Result;
	
	//GetByFilterAsync
	var filter = CustomerRetriver.Create().SetFirstName(firstName);	
	var customersResult = DbCRUD.Retriever<Customer>().GetByFilterAsync(filter).Result;
	
	// GetByAction
	var customersResult = DbCRUD.Retriever<Customer>()
		.GetByAction(r => r.FirstName.ToUpper().Contains("Red".ToUpper())).Result;
	
	
	//Here is class example for making filter
	//public class CustomerRetriver : BaseRetriver<Customer, LailsDbContext>
	//{

	//	public static CustomerRetriver Create() => new CustomerRetriver();
	//	public override IQueryable<Customer> QueryDefinition()
	//	{
	//		var query = Query;

	//		if (FirstName?.Length > 0)
	//		{
	//			query = query.Where(r => r.FirstName == FirstName);
	//		}
	//		return query;
	//	}


	//	public string FirstName { get; private set; }
	//	public CustomerRetriver SetFirstName(string firstName)
	//	{
	//		FirstName = firstName;
	//		return this;
	//	}
	//}
``` 
