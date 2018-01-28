using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using BookEditor.Data.Models;
using BookEditor.Data.Repositories;

namespace BookEditorSPA.Controllers
{
	public class AuthorsController : BaseApiController
	{
		public AuthorsController(IDataContext dataContext)
			: base(dataContext)
		{
		}

		public IHttpActionResult GetAuthors()
		{
			//Att Task  await
			var books = _dataContext.GetAuthors();
			if (books == null || !books.Any())
				return NotFound();
			return Ok(books);
		}

		 
		[HttpDelete]
		public IHttpActionResult Delete(long id)
		{
			_dataContext.DeleteBook(id);
			return Ok();
		}
	 

		[HttpPut]
		public IHttpActionResult Put(AuthorModel author)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);
			try
			{
				_dataContext.EditAuthor(author);
			}
			catch (InvalidOperationException e)
			{
				
			}
			return Ok();
		}
	}
}
