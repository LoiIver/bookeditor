using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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

				// This illustrates how to get the file names.
				//foreach (MultipartFileData file in provider.FileData)
				//{
				//	Trace.WriteLine(file.Headers.ContentDisposition.FileName);
				//	Trace.WriteLine("Server file path: " + file.LocalFileName);
				//}

				//// check if files are on the request.
				//if (provider.FileStreams.Count == 0)
				//{
				//	// return return error response               
				//}

				IList<string> uploadedFiles = new List<string>();

				foreach (KeyValuePair<string, Stream> file in provider.FileStreams)
				{
					// get file name and file stream
					byte[] photo;
					string fileName = file.Key;
					using (Stream stream = file.Value)
					{
						using (BinaryReader reader = new BinaryReader(stream))
						{
							photo = reader.ReadBytes((int)stream.Length);
						}
					}
					return Request.CreateResponse(HttpStatusCode.OK);
			}
			catch (System.Exception e)
			{
				return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
			}
		}

	}
}

