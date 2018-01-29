using System.Collections.Generic;
using System.Linq;
using BookEditor.Data.Contracts;
using BookEditor.Data.Models;

namespace BookEditor.Data.Repositories
{
	public class AuthorRepository  : IAuthorRepository
	{

		private List<Author> _items  = new List<Author>();
		public List<Author> Get()
		{
			return _items;
		}

		public long Add(Author t)
		{
			var id = (_items.Any() ? _items.Max(a => a.AuthorId) : 0) + 1;
			t.AuthorId = id;
			_items.Add(t);
			return id;
		}

		public   Author Get(long id)
		{
			return _items.Single(t => t.AuthorId == id);
		}

		public void Delete(long id)
		{
			var author = Get(id);
			_items.Remove(author);
		}
 
		public void Update(Author t)
		{
			var author = Get(t.AuthorId);
			author.FirstName = t.FirstName;
			author.LastName = t.LastName;
		}
	}
}

