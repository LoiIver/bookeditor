using System;
using System.Collections.Generic;
using System.Linq;
using BookEditor.Data.Contracts;
using BookEditor.Data.Models;

namespace BookEditor.Data.Repositories
{
	public class AuthorRepository  : IAuthorRepository
	{
		private static readonly List<Author> _authors = new List<Author>
		{
			new Author{AuthorId = 1, FirstName = "Альбер", LastName= "Камю"},
			new Author{AuthorId = 2, FirstName = "Александр", LastName= "Дюма"},
			new Author{AuthorId = 3, FirstName = "Виктор", LastName= "Гюго"},
			new Author{AuthorId = 4, FirstName = "Фридрих", LastName= "Ницше"},
			new Author{AuthorId = 5, FirstName = "Герман", LastName= "Гессе"},
			new Author{AuthorId = 6, FirstName = "Фома", LastName= "Аквинский"},
			new Author{AuthorId = 7, FirstName = "Иммануил", LastName= "Кант"},
			new Author{AuthorId = 8, FirstName = "Шарль", LastName= "Монтескье"},
			new Author{AuthorId = 9, FirstName = "Жан-Поль", LastName= "Сартр"},
			new Author{AuthorId = 10, FirstName = "Зигмунд", LastName= "Фрейд"},
		};

		public List<Author> Get()
		{
			return _authors;
		}

		public   Author Get(long id)
		{
			return _authors.Single(t => t.AuthorId == id);
		}

		public void Delete(long id)
		{
			throw new NotImplementedException();
		}
 

		public void Update(Author t)
		{
			throw new NotImplementedException();
		}
	}
}

