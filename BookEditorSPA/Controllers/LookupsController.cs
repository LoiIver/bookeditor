using System.Web.Http;
using BookEditor.Data.Repositories;

//using BookEditor.Models;


namespace BookEditorSPA.Controllers
{
	public class LookupsController : ApiController
	{
		public IDataContext _dataContext { get; set; }

		public LookupsController (IDataContext dataContext)
		{
			_dataContext = dataContext;
		}

		public IHttpActionResult Get()
		{
			var authors = _dataContext.GetAuthors();
			var pubHouses = _dataContext.GetPubHouses();
			return Ok(new {   authors,  pubHouses });
		}
  
	}
}
