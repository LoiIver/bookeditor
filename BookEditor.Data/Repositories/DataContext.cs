using System;
using System.Collections.Generic;
using System.Linq;
using BookEditor.Data.Contracts;
using BookEditor.Data.Models;

namespace BookEditor.Data.Repositories
{
	public class DataContext : IDataContext
	{
		private IBookRepository Books { get; set; }
		private IAuthorRepository Authors { get; set; }
		private IBookAuthorRepository BookAuthors { get; set; }
		private IPubHouseRepository PubHouses { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DataContext(IBookRepository books,
			IAuthorRepository authors,
			IBookAuthorRepository bookAuthors,
			IPubHouseRepository pubHouses
			)
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
			Book siz = new Book
			{
				BookId = 1,
				Title = "Миф о Сизифе",
				NumPages = 255,
				PubHouseId = pros.PubHouseId,
				ISBN = "",
				Illustration = "",
				PublishYear = 1998
			};
			Book first = new Book
			{
				BookId = 2,
				Title = "Первый человек",
				NumPages = 190,
				PubHouseId = clever.PubHouseId,
				ISBN = "",
				Illustration = "",
				PublishYear = 2005
			};

			Book zar = new Book
			{
				BookId = 3,
				Title = "Там говорил Заратустра",
				NumPages = 217,
				PubHouseId = azb.PubHouseId,
				ISBN = "",
				Illustration = "",
				PublishYear = 2010
			};

			Book three = new Book
			{
				BookId = 4,
				Title = "Три мушкетёра",
				NumPages = 473,
				PubHouseId = pros.PubHouseId,
				ISBN = "",
				Illustration = "",
				PublishYear = 1965
			};

			Book monday = new Book
			{
				BookId = 5,
				Title = "Понедельник начинается в субботу",
				NumPages = 230,
				PubHouseId = pros.PubHouseId,
				ISBN = "",
				Illustration = "",
				PublishYear = 1978
			};

			Book outlaw = new Book
			{
				BookId = 6,
				Title = "Отверженные",
				NumPages = 890,
				PubHouseId = ped.PubHouseId,
				ISBN = "",
				Illustration = "",
				PublishYear = 1901
			};

			Books.Add(siz);
			Books.Add(first);
			Books.Add(zar);
			Books.Add(three);
			Books.Add(monday);
			Books.Add(outlaw);
			#endregion books

			#region book authors
			BookAuthors.Add(new BookAuthors { BookAuthorId = 1, BookId = siz.BookId, AuthorId = kamu.AuthorId });
			BookAuthors.Add(new BookAuthors { BookAuthorId = 2, BookId = first.BookId, AuthorId = kamu.AuthorId });
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

		public List<BookModel> GetBooks()
		{
			var books = Books.Get();
			if (books == null || !books.Any())
				return Enumerable.Empty<BookModel>().ToList();

			var pubHouseIds = books.Select(t => t.PubHouseId);
			var pubHouses = PubHouses.Get().Where(t => pubHouseIds.Contains(t.PubHouseId));

			var bookIds = books.Select(t => t.BookId);
			var t1 = BookAuthors.Get().Where(t => bookIds.Contains(t.BookId));
			var t2 = Authors.Get();
			var authors = t1.Join(t2,
				ba => ba.AuthorId,
				a => a.AuthorId,
			
				( ba, a) =>
				new
				{
					a.AuthorId,
					ba.BookId,
					a.FirstName,
					a.LastName
				});

			var list = new List<BookModel>();
			books.ForEach(t =>
			{
				var thisBookAuthors = authors.Where(a => a.BookId == t.BookId).ToList();
				list.Add(new BookModel()
				{
					BookId = t.BookId,
					Title = t.Title,
					NumPages = t.NumPages,
					PublishYear = t.PublishYear,
					ISBN = t.ISBN,
					Illustration = t.Illustration,
					PubHouseId = t.PubHouseId,
					PubHouseName = pubHouses.Single(p => p.PubHouseId == t.PubHouseId).Name,
					Authors = thisBookAuthors.Select(q => q.AuthorId).ToList(),
					AuthorsNames = String.Join(", ", thisBookAuthors.Select(aa => $"{aa.LastName} {aa.FirstName}"))
				});
			});
			return list;
		}

		public List<Author> GetAuthors()
		{
			return Authors.Get();
		}

		public List<PubHouse> GetPubHouses()
		{
			return PubHouses.Get();
		}

		public BookModel GetBook(long id)
		{
			var book = Books.Get(id);
			var pubHouse = PubHouses.Get().Single(t => t.PubHouseId == book.PubHouseId);
			var authors = Authors.Get()
				.Join(BookAuthors.Get().Where(q => q.BookId == book.BookId), a => a.AuthorId, ba => ba.AuthorId, (a, ba) =>
					new { a.AuthorId, ba.BookId, a.FirstName, a.LastName }).ToList();

			var bookModel = new BookModel()
			{
				BookId = book.BookId,
				Title = book.Title,
				NumPages = book.NumPages,
				PublishYear = book.PublishYear,
				ISBN = book.ISBN,
				Illustration = book.Illustration,
				PubHouseId = book.PubHouseId,
				PubHouseName = pubHouse.Name,
				Authors = authors.Select(q => q.AuthorId).ToList(),
				AuthorsNames = string.Join(", ", authors.Select(aa => $"{aa.LastName} {aa.FirstName}"))
			};
			return bookModel;
		}

		public void EditBook(BookModel book)
		{
			BookAuthors.DeleteBookAuthors(book.BookId);
			book.Authors.ForEach(t=>
				BookAuthors.Add(new BookAuthors { BookId = book.BookId, AuthorId = t}));

			Books.Update(new Book {
				BookId = book.BookId,
				PubHouseId = book.PubHouseId,
				PublishYear = book.PublishYear,
				NumPages = book.NumPages,
				ISBN = book.ISBN,
				Illustration = book.Illustration,
				Title = book.Title
			}); 
			
			
		}
	}
}
