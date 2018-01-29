using System.Collections.Generic;
using BookEditor.Data.DataModels;
using BookEditor.Data.Models;

namespace BookEditor.Data.Repositories
{
	public interface IDataContext
	{
		void DeleteBook(long id);
		IEnumerable<BookModel> GetBooks();
		IEnumerable<Author> GetAuthors();
		IEnumerable<PubHouse> GetPubHouses();
		BookModel GetBook(long id);
		void EditBook(BookModel book);
		void EditAuthor(AuthorModel author);
		void AddBook(BookModel book);
		void DeleteAuthor(long id);
	}
}
