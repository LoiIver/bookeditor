using System.Web.Http;
using BookEditor.Data.Repositories;

namespace BookEditorSPA.Controllers
{
	public class LookupsController : BaseApiController
	{
		public LookupsController(IDataContext dataContext)
			: base(dataContext)
		{
		}
		public IHttpActionResult Get()
		{
			var authors = _dataContext.GetAuthors();
			var pubHouses = _dataContext.GetPubHouses();
			return Ok(new {   authors,  pubHouses });
		}
	}
}
