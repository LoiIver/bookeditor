using System;
using System.Collections.Generic;
using System.Linq;
using BookEditor.Data.Contracts;
using BookEditor.Data.DataModels;
using BookEditor.Data.Models;


namespace BookEditor.Data.Repositories
{
	public class DataContext : IDataContext
	{
		private IBookRepository Books { get; set; }
		private IAuthorRepository Authors { get; set; }
		private IBookAuthorRepository BookAuthors { get; set; }
		private IPubHouseRepository PubHouses { get; set; }

		public DataContext(IBookRepository books,
			IAuthorRepository authors,
			IBookAuthorRepository bookAuthors,
			IPubHouseRepository pubHouses)
		{
			Books = books;
			Authors = authors;
			BookAuthors = bookAuthors;
			PubHouses = pubHouses;
			Populate();
		}

		private void Populate()
		{
			#region pubhouses
			var pros = new PubHouse { PubHouseId = 1, Name = "Просвещение" };
			var ped = new PubHouse { PubHouseId = 2, Name = "Педагогическая книга" };
			var clever = new PubHouse { PubHouseId = 3, Name = "Клевер" };
			var act = new PubHouse { PubHouseId = 4, Name = "АСТ" };
			var azb = new PubHouse { PubHouseId = 5, Name = "Азбука" };

			PubHouses.Add(pros);
			PubHouses.Add(ped);
			PubHouses.Add(clever);
			PubHouses.Add(act);
			PubHouses.Add(azb);
			#endregion pubHouses

			#region authors
			var kamu = new Author { AuthorId = 1, FirstName = "Альбер", LastName = "Камю" };
			var duma = new Author { AuthorId = 2, FirstName = "Александр", LastName = "Дюма" };
			var gugo = new Author { AuthorId = 3, FirstName = "Виктор", LastName = "Гюго" };
			var nizh = new Author { AuthorId = 4, FirstName = "Фридрих", LastName = "Ницше" };
			var gess = new Author { AuthorId = 5, FirstName = "Герман", LastName = "Гессе" };
			var akv = new Author { AuthorId = 6, FirstName = "Фома", LastName = "Аквинский" };
			var kant = new Author { AuthorId = 7, FirstName = "Иммануил", LastName = "Кант" };
			var mont = new Author { AuthorId = 8, FirstName = "Шарль", LastName = "Монтескье" };
			var sart = new Author { AuthorId = 9, FirstName = "Жан-Поль", LastName = "Сартр" };
			var frei = new Author { AuthorId = 10, FirstName = "Зигмунд", LastName = "Фрейд" };
			var ark = new Author { AuthorId = 11, FirstName = "Аркадий", LastName = "Стругацкий" };
			var bor = new Author { AuthorId = 12, FirstName = "Борис", LastName = "Стругацкий" };

			Authors.Add(kamu);
			Authors.Add(duma);
			Authors.Add(gugo);
			Authors.Add(nizh);
			Authors.Add(gess);
			Authors.Add(akv);
			Authors.Add(kant);
			Authors.Add(mont);
			Authors.Add(sart);
			Authors.Add(frei);
			Authors.Add(ark);
			Authors.Add(bor);
			#endregion authors

			#region books
			var siz = new Book
			{
				BookId = 1,
				Title = "Миф о Сизифе",
				NumPages = 384,
				PubHouseId = pros.PubHouseId,
				ISBN = "978-5-17-083384-9",
				PublishYear = 2014
			};
			var sob = new Book
			{
				BookId = 2,
				Title = "Собор Парижской Богоматери",
				NumPages = 656,
				PubHouseId = clever.PubHouseId,
				ISBN = "978-5-699-38153-1",
				PublishYear = 2011
			};

			var zar = new Book
			{
				BookId = 3,
				Title = "Так говорил Заратустра",
				NumPages = 320,
				PubHouseId = azb.PubHouseId,
				ISBN = "978-5-17-082222-5",
				PublishYear = 2017
			};

			var three = new Book
			{
				BookId = 4,
				Title = "Три мушкетёра",
				NumPages = 958,
				PubHouseId = pros.PubHouseId,
				ISBN = "5-224-04981-4",
				PublishYear = 2005
			};

			var monday = new Book
			{
				BookId = 5,
				Title = "Понедельник начинается в субботу",
				NumPages = 320,
				PubHouseId = pros.PubHouseId,
				ISBN = "978-5-17-090334-4",
				PublishYear = 2017
			};

			var outlaw = new Book
			{
				BookId = 6,
				Title = "Отверженные",
				NumPages = 1248,
				PubHouseId = ped.PubHouseId,
				ISBN = "978-5-389-06864-3",
				PublishYear = 2014
			};

			Books.Add(siz);
			Books.Add(sob);
			Books.Add(zar);
			Books.Add(three);
			Books.Add(monday);
			Books.Add(outlaw);
			#endregion books

			#region book authors
			BookAuthors.Add(new BookAuthors { BookAuthorId = 1, BookId = siz.BookId, AuthorId = kamu.AuthorId });
			BookAuthors.Add(new BookAuthors { BookAuthorId = 2, BookId = sob.BookId, AuthorId = gugo.AuthorId });
			BookAuthors.Add(new BookAuthors { BookAuthorId = 3, BookId = zar.BookId, AuthorId = nizh.AuthorId });
			BookAuthors.Add(new BookAuthors { BookAuthorId = 4, BookId = three.BookId, AuthorId = duma.AuthorId });
			BookAuthors.Add(new BookAuthors { BookAuthorId = 5, BookId = outlaw.BookId, AuthorId = gugo.AuthorId });
			BookAuthors.Add(new BookAuthors { BookAuthorId = 6, BookId = monday.BookId, AuthorId = ark.AuthorId });
			BookAuthors.Add(new BookAuthors { BookAuthorId = 7, BookId = monday.BookId, AuthorId = bor.AuthorId });
			#endregion book authors
		}

		public void DeleteBook(long id)
		{
			BookAuthors.DeleteBookAuthors(id);
			Books.Delete(id);
		}

		public IEnumerable<BookModel> GetBooks()
		{
			var books = Books.Get()?.ToList();
			if (books == null || !books.Any())
				return Enumerable.Empty<BookModel>().ToList();

			var pubHouseIds = books.Select(t => t.PubHouseId);
			var pubHouses = PubHouses.Get().Where(t => pubHouseIds.Contains(t.PubHouseId));

			var bookIds = books.Select(t => t.BookId);

			var authors = BookAuthors.Get().Where(t => bookIds.Contains(t.BookId)).Join(Authors.Get(),
				ba => ba.AuthorId,
				a => a.AuthorId,
				(ba, a) =>
				new
				{
					ba.BookId,
					author = a
				});

			var list = new List<BookModel>();
			books.ForEach(t =>
			{
				var thisBookAuthors = authors.Where(a => a.BookId == t.BookId).Select(q => q.author).ToList();
				var pubHouse = pubHouses.SingleOrDefault(p => p.PubHouseId == t.PubHouseId);
				list.Add(new BookModel(t, thisBookAuthors, pubHouse));

			});
			return list;
		}

		public IEnumerable<Author> GetAuthors()
		{
			return Authors.Get();
		}

		public IEnumerable<PubHouse> GetPubHouses()
		{
			return PubHouses.Get();
		}

		public BookModel GetBook(long id)
		{
			var book = Books.GetById(id);

			var pubHouse = book.PubHouseId.HasValue ? PubHouses.GetById(book.PubHouseId.Value) : null;

			var authors = Authors.Get()
				.Join(BookAuthors.Get().Where(q => q.BookId == book.BookId), a => a.AuthorId, ba => ba.AuthorId, (a, ba) =>
					a).ToList();
			var bookModel = new BookModel(book, authors, pubHouse);
			return bookModel;
		}

		public void EditBookImage(long id, byte[] img)
		{
			var book = Books.GetById(id);
			book.Illustration = img;
		}

		public void EditBook(BookModel book)
		{
			BookAuthors.DeleteBookAuthors(book.BookId);
			book.Authors.ForEach(t =>
				BookAuthors.Add(new BookAuthors { BookId = book.BookId, AuthorId = t }));

			Books.Update(new Book
			{
				BookId = book.BookId,
				PubHouseId = book.PubHouseId,
				PublishYear = book.PublishYear,
				NumPages = book.NumPages,
				ISBN = book.ISBN,
				Title = book.Title
			});
		}

		public void EditAuthor(AuthorModel author)
		{
			throw new NotImplementedException();
		}

		public void AddBook(BookModel book)
		{
			var id = Books.Add(new Book
			{
				PubHouseId = book.PubHouseId,
				PublishYear = book.PublishYear,
				NumPages = book.NumPages,
				ISBN = book.ISBN,
				Title = book.Title
			});
			book.Authors.ForEach(t =>
				BookAuthors.Add(new BookAuthors { BookId = id, AuthorId = t }));
		}

		public void DeleteAuthor(long id)
		{
			if (BookAuthors.Get().Any(t => t.AuthorId == id))
				throw new InvalidOperationException();
			Authors.Delete(id);
		}
	}
}
