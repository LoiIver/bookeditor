using System;
using System.Collections.Generic;
using System.Linq;
using BookEditor.Data.Contracts;
using BookEditor.Data.Models;

namespace BookEditor.Data.Repositories
{
	public class BookRepository : IBookRepository
	{

		internal static List<Book> Books;

		public  List<Book> Get()
		{
			return Books;
		}

		public   Book Get(long id)
		{
			return Books.Single(t => t.BookId == id);
		}

		public void Update(Book t)
		{
			throw new NotImplementedException();
		}

		public void Delete(long id)
		{
			var book = Get(id);
			Books.Remove(book);
		}
	}
}
