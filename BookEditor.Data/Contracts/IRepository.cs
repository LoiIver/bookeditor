using System.Collections.Generic;

namespace BookEditor.Data.Contracts
{
	public interface IRepository<T>  where  T: class
	{
		List<T> Items { get; set; }
		T Get(long id);
		List<T> Get();
		void Add(T t);
		void Update(T t);
		void Delete(long id);
	}
}
