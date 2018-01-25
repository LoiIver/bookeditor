using System.Web.Mvc;

namespace BookEditorSPA.Controllers
{
	 
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			return View();
		}
	}
}
