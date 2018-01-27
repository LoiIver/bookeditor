using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookEditor.Data.Contracts;
using BookEditor.Data.Models;

namespace BookEditor.Data.Repositories
{
	public class DataContext
	{
		public IRepository<Book> Books;
		public IRepository<Author> Authors;
		public IRepository<BookAuthors> BookAuthors;
		public IRepository<PubHouse> PubHouses;

		public DataContext 
		{
			Books.
	}
	}
}
