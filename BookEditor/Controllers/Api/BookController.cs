using System.Collections.Generic;
using System.Web.Http;
using BookEditor.Data.Models;
using BookEditor.Data.Repositories;
 

namespace BookEditor.Controllers.Api
{
	public class BookController : ApiController
	{
		private readonly IBookRepository _bookRepository;

		public BookController(IBookRepository repository)
		{
			_bookRepository = repository;
		}

		public IEnumerable<Book> GetBooks()
		{
			//Att Task  await
			return _bookRepository.GetBooks();
		}
	}
}
