using BookEditor.Data.DataModels;

namespace BookEditor.Data.Contracts
{
	public interface IBookAuthorRepository : IRepository<BookAuthors>
	{
		void DeleteBookAuthors(long bookId);
	}
}
