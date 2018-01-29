using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using BookEditor.Data.Models;
using BookEditor.Data.Repositories;


namespace BookEditor.Web.Controllers
{
	public class BooksController : BaseApiController
	{
		public BooksController(IDataContext dataContext)
			: base(dataContext)
		{
		}
		public IHttpActionResult GetBooks()
		{
			try
			{
				var books = _dataContext.GetBooks()?.ToList();
				if (books == null || !books.Any())
					return NotFound();
				return Ok(books);
			}
			catch
			{
				return BadRequest("Невозможно отобразить информацию о книгах");
			}
		}

		public IHttpActionResult Get(long id)
		{
			try
			{
				var book = _dataContext.GetBook(id);
				return Ok(book);
			}
			catch
			{
				return BadRequest("Невозможно отобразить информацию о книге");
			}

		}


		public IHttpActionResult Delete(long id)
		{
			try
			{
				_dataContext.DeleteBook(id);
				return Ok("Книга удалена успешно");
			}
			catch
			{
				return BadRequest("Невозможно удалить книгу");
			}
		}

		[ValidateModel]
		public IHttpActionResult Put(BookModel book)
		{
			try
			{
				if (!ModelState.IsValid)
					return BadRequest(ModelState);

				_dataContext.EditBook(book);
				return Ok("Информация обновлена успешно");
			}
			catch
			{
				return BadRequest($"Невозможно отредактировать информацио о книге \"{book.Title}\"");
			}
		}

		[ValidateModel]
		public IHttpActionResult Post(BookModel book)
		{
			try
			{
				if (!ModelState.IsValid)
					return BadRequest(ModelState);

				_dataContext.AddBook(book);
				return Ok($"Информация о книге \"{book.Title}\" добавлена");
			}
			catch
			{
				return BadRequest($"Невозможно добавить информацио о книге {book.Title}");
			}
		}

		[Route("api/books/upload/{id}")]
		public async Task<HttpResponseMessage> PostFormData(int id)
		{
			// Check if the request contains multipart/form-data.
			if (!Request.Content.IsMimeMultipartContent())
			{
				throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
			}

		//	string root = HttpContext.Current.Server.MapPath("~/App_Data");
		//	var provider = new MultipartFormDataStreamProvider(root);

			try
			{
				// Read the form data.
				byte[]  file = 
					await Request.Content.ReadAsByteArrayAsync();
				_dataContext.EditBookImage(id,file);
				return Request.CreateResponse(HttpStatusCode.OK);
			}
			catch (System.Exception e)
			{
				return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
			}
		}

	}
}

