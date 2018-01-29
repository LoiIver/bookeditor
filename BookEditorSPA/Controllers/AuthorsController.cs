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

		public IHttpActionResult Get()
		{
			//Att Task  await
			var authors = _dataContext.GetAuthors();
			if (authors == null || !authors.Any())
				return NotFound();
			return Ok(authors);
		}
	 

		[HttpDelete]
		public IHttpActionResult Delete(long id)
		{
			_dataContext.DeleteAuthor(id);
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
