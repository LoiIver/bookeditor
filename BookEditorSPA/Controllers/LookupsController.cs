using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using BookEditor.Data.Models;
using BookEditor.Data.Contracts;

//using BookEditor.Models;


namespace BookEditorSPA.Controllers
{
	public class LookupsController : ApiController
	{
		private readonly IAuthorRepository _authors;
		private readonly IPubHouseRepository _pubHouses;

		public LookupsController(IAuthorRepository authors, IPubHouseRepository pubHouses)
		{
			_authors = authors;
			_pubHouses = pubHouses;
		}

		public IHttpActionResult Get()
		{
			var authors = _authors.Get();
			var pubHouses = _pubHouses.Get();
			return Ok(new {   authors,  pubHouses });
		}
  
	}
}
