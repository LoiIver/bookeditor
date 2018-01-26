using System.Collections.Generic;

namespace BookEditor.Data.Contracts
{
	public interface IRepository<T>  where  T: class
	{
		T Get(long id);

		List<T> Get();

		void Update(T t);
		void Delete(long id);
	}
}
