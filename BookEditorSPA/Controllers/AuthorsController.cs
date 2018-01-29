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
			try
			{
				var authors = _dataContext.GetAuthors()?.ToList();
				if (authors == null || !authors.Any())
					return NotFound();
				return Ok(authors);
			}
			catch
			{
				return BadRequest("Невозможно отобразить информацию об авторах");
			}
		}
 
		public IHttpActionResult Delete(long id)
		{
			try
			{
				_dataContext.DeleteAuthor(id);
				return Ok("Информация об авторе удалена успешно");
			}
			catch
			{
				return BadRequest("Невозможно удалить информацию об авторе");
			}
		}

		public IHttpActionResult Put(AuthorModel author)
		{
			try
			{
				if (!ModelState.IsValid)
					return BadRequest(ModelState);

				_dataContext.EditAuthor(author);
				return Ok("Информация об авторе обновлена успешно");
			}
			catch
			{
				return BadRequest($"Невозможно отредактировать информацио об авторе \"{author?.LastName}\"");
			}
		}

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
				return BadRequest($"Невозможно добавить информацио о книге {book?.Title}");
			}
		}
	}
}
