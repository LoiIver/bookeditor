using System.Web.Http;
using BookEditor.Data.Repositories;

namespace BookEditor.Web.Controllers
{
	public class BaseApiController : ApiController
	{
		protected readonly IDataContext _dataContext;

		public BaseApiController(IDataContext dataContext)
		{
			_dataContext = dataContext;
		}
	}
}
