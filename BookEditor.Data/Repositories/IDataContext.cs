using System.Collections.Generic;
using BookEditor.Data.Models;

namespace BookEditor.Data.Repositories
{
	public interface IDataContext
	{
		void DeleteBook(long id);
		List<BookModel> GetBooks();
		List<Author> GetAuthors();
		List<PubHouse> GetPubHouses();
		BookModel GetBook(long id);
		void EditBook(BookModel book);
		void EditAuthor(AuthorModel author);
		void AddBook(BookModel book);
		void DeleteAuthor(long id);
	}
}
