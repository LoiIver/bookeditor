using System.Collections.Generic;
using BookEditor.Data.Models;

namespace BookEditor.Data.Repositories
{
	public interface IBookRepository : IRepository
	{
		IEnumerable<Book> GetBooks();
		Book GetBook(int id);
	}

}