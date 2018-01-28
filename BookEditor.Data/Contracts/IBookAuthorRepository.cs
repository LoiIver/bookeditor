using BookEditor.Data.Models;

namespace BookEditor.Data.Contracts
{
	public interface IBookAuthorRepository : IRepository<BookAuthors>
	{
		void DeleteBookAuthors(long bookId);
	}
}
