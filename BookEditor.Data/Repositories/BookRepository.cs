using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using BookEditor.Data.Contracts;
using BookEditor.Data.Models;

namespace BookEditor.Data.Repositories
{
	public class BookRepository : IBookRepository
	{

		private readonly List<Book> _books;

		public BookRepository(IAuthorRepository authorRepo, IPubHouseRepository pubRepo)
		{
			_books = new List<Book>();
			var authors = authorRepo.Get().ToArray();
			var pubHouse = pubRepo.Get().ToArray();
			_books.Add(new Book
			{
				BookId = 1,
				Title = "Миф о Сизифе",
				Authors = new List<Author>() {authors[0] /*Камю*/},
				NumPages = 451,
				PubHouse = pubHouse[0],
				PublishYear = 1942,
				ISBN = "",
			});
			_books.Add(new Book
			{
				BookId = 1,
				Title = "Первый человек",
				Authors = new List<Author>() { authors[0] /*Камю*/},
				NumPages = 451,
				PubHouse = pubHouse[0] /*Просвещение*/,
				PublishYear = 1942,
				ISBN = "",
			});



			new Book 
					new Book {BookId = 2 , Title="Первый человек", },
				};

		}

		public  List<Book> Get()
		{
			return _books;
		}

		public   Book Get(long id)
		{
			return _books.Single(t => t.BookId == id);
		}

		public void Update(Book t)
		{
			throw new NotImplementedException();
		}

		public void Delete(long id)
		{
			throw new NotImplementedException();
		}
	}
}
