using System.Collections.Generic;
using System.Linq;
using BookEditor.Data.Contracts;
using BookEditor.Data.DataModels;

namespace BookEditor.Data.Repositories
{
	public class BookRepository : IBookRepository
	{
		private readonly List<Book> _items = new List<Book>();

		public  IEnumerable<Book> Get()
		{
			return _items;
		}

		public long Add(Book t)
		{
			var id = (_items.Any() ? _items.Max(a => a.BookId) : 0) + 1;
			t.BookId = id;
			_items.Add(t);
			return id;
		}

		public  Book GetById(long id)
		{
			return _items.Single(t => t.BookId == id);
		}

		public void Update(Book t)
		{
			var book = GetById(t.BookId);
			book.PubHouseId = t.PubHouseId;
			book.PublishYear = t.PublishYear;
			book.NumPages = t.NumPages;
			book.ISBN = t.ISBN;
			book.Illustration = t.Illustration;
			book.Title = t.Title;
		}

		public void Delete(long id)
		{
			var book = GetById(id);
			_items.Remove(book);
		}
	}
}
