using System.Collections.Generic;

namespace BookEditor.Data.Contracts
{
	public interface IRepository<T>  where  T: class
	{		
		IEnumerable<T> Get();
		T GetById(long id);
		long Add(T t);
		void Update(T t);
		void Delete(long id);
	}
}
