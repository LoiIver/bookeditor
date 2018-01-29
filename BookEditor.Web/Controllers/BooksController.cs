using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
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
			try
			{
				if (!Request.Content.IsMimeMultipartContent())
				{
					Request.CreateResponse(HttpStatusCode.BadRequest);
				}

				var provider = new MultipartFormDataMemoryStreamProvider();
				await Request.Content.ReadAsMultipartAsync(provider).ContinueWith(p =>
				{
					var result = p.Result;
					foreach (var stream in result.Contents.Where((content, idx) => result.IsStream(idx)))
					{
						var file = stream.ReadAsByteArrayAsync();
						_dataContext.EditBookImage(id, file.Result);
					}
				});

				return Request.CreateResponse(HttpStatusCode.OK);
			}
			catch (Exception e)
			{
				return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
			}
		}

	}
}

