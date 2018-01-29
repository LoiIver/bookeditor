using System;
using System.Collections.Generic;
using System.Linq;
using BookEditor.Data.Contracts;
using BookEditor.Data.DataModels;

namespace BookEditor.Data.Repositories
{
	public class BookAuthorRepository : IBookAuthorRepository
	{
		private  List<BookAuthors> _items = new List<BookAuthors>();

		public void Update(BookAuthors t)
		{
			throw new NotImplementedException();
		}

		public void Delete(long id)
		{
			throw new NotImplementedException();
		}

		public void DeleteBookAuthors(long bookId)
		{
			_items.RemoveAll(t => t.BookId == bookId);
		}

		public IEnumerable<BookAuthors>  Get()
		{
			return _items;
		}

		public long Add(BookAuthors t)
		{
			long id = (_items.Any() ? _items.Max(a => a.BookAuthorId) : 0) + 1;
			t.BookAuthorId = id;
			_items.Add(t);
			return id;
		}

		public BookAuthors GetById(long id)
		{
			return _items.Single(t => t.BookAuthorId == id);
		}
	}
}
