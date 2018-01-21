using System.Collections.Generic;
using System.Linq;
using BookEditor.Data.Models;

namespace BookEditor.Data.Repositories
{
	public class BookRepository : IBookRepository
	{
		private static readonly List<Book> Books
			= new List<Book>
				{
					new Book {Title="Война и мир",BookId = 1  },
					new Book {Title="Война и мир",BookId =	2  }
				};
		public IEnumerable<Book> GetBooks()
		{
			return Books;
		}

		public Book GetBook(int id)
		{
			return Books.SingleOrDefault(t => t.BookId == id);
		}
	}
}
