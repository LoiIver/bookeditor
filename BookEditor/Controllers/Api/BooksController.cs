using System.Linq;
using System.Web.Http;
using BookEditor.Data.Contracts;
using BookEditor.Models;


namespace BookEditor.Controllers.Api
{
	public class BooksController : ApiController
	{
		private readonly IBookRepository _bookRepository;

		public BooksController(IBookRepository repository)
		{
			_bookRepository = repository;
		}

		public IHttpActionResult GetBooks()
		{

			//Att Task  await
			var books = _bookRepository.Get();
			if (books == null || books.Any())
				return NotFound();
			return Ok(books);
		}

		public IHttpActionResult GetBook(int id)
		{
			var book =
				_bookRepository.Get(id);
			//Att Task  await
			if (book == null)
				return NotFound();
			return Ok(book);
		}

		[HttpPost]
		public IHttpActionResult EditBook(BookModel book)
		{
			//if (book.BookId == 0 ) 

			///	var book = _bookRepository.GetBook(book);
			//Att Task  await
			//		return _bookRepository.GetBook(id);
			if (!ModelState.IsValid)
			{
				return BadRequest(this.ModelState);
			}

//			answer.UserId = User.Identity.Name;

	//		var isCorrect = await this.StoreAsync(answer);
			return this.Ok<bool>(true);
		}
	}
}
