using System.Web.Mvc;

namespace BookEditor.Web.Controllers
{
	 
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			return View();
		}
	}
}
