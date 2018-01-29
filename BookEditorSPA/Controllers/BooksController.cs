using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using BookEditor.Data.Models;
using BookEditor.Data.Repositories;


namespace BookEditorSPA.Controllers
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

		[Route("api/books/upload")]
		public async Task<HttpResponseMessage> PostFormData()
		{
			// Check if the request contains multipart/form-data.
			if (!Request.Content.IsMimeMultipartContent())
			{
				throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
			}

			string root = HttpContext.Current.Server.MapPath("~/App_Data");
			var provider = new MultipartFormDataStreamProvider(root);

			try
			{
				// Read the form data.
				await Request.Content.ReadAsMultipartAsync(provider);

				//This illustrates how to get the file names.
				foreach (MultipartFileData file in provider.FileData)
				{
					Trace.WriteLine(file.Headers.ContentDisposition.FileName);
					Trace.WriteLine("Server file path: " + file.LocalFileName);
				}

				//// check if files are on the request.
				//if (provider.FileStreams.Count == 0)
				//{
				//	// return return error response               
				//}

				//IList<string> uploadedFiles = new List<string>();

				//foreach (KeyValuePair<string, Stream> file in provider.FileStreams)
				//{
				//	// get file name and file stream
				//	byte[] photo;
				//	string fileName = file.Key;
				//	using (Stream stream = file.Value)
				//	{
				//		using (BinaryReader reader = new BinaryReader(stream))
				//		{
				//			photo = reader.ReadBytes((int)stream.Length);
				//		}
				//	}
				return Request.CreateResponse(HttpStatusCode.OK);
			}
			catch (System.Exception e)
			{
				return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
			}
		}

	}
}

