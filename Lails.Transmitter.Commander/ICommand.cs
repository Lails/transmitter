using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lails.Transmitter.Commander
{
	public interface ICommand<TData> where TData : class
	{
		Task CreateAsync(TData data);
		Task UpdateAsync(TData data);
		Task DeleteAsync(TData data);
	}
}
