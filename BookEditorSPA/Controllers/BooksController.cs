using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using BookEditor.Data.Models;
using BookEditor.Data.Repositories;

//using BookEditor.Models;


namespace BookEditorSPA.Controllers
{
	public class BooksController : ApiController
	{
		private readonly IDataContext _dataContext;

		public BooksController(IDataContext dataContext)
		{
			_dataContext = dataContext;
		}

		public IHttpActionResult GetBooks()
		{
			//Att Task  await
			var books = _dataContext.GetBooks();
			if (books == null || !books.Any())
				return NotFound();
			return Ok(books);
		}

		public IHttpActionResult Get(long id)
		{
			var book =
				_dataContext.GetBook(id);
			//Att Task  await
			if (book == null)
				return NotFound();
			return Ok(book);
		}

		[HttpDelete]
		public IHttpActionResult Delete(long id)
		{
			_dataContext.DeleteBook(id);
			return Ok();
		}
	 

		[HttpPut]
		public IHttpActionResult Put(BookModel book)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			_dataContext.EditBook(book);
			return Ok();
		}
	}
}
