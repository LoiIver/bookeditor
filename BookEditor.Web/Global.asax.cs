using System.Web;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using BookEditor.Web.Windsor;
using Castle.Windsor;
using Castle.Windsor.Installer;

namespace BookEditor.Web
{
	public class MvcApplication : HttpApplication
	{
		private static IWindsorContainer _container;

		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();
			GlobalConfiguration.Configure(WebApiConfig.Register);
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);
			BootstrapContainer();
		}

		private static void BootstrapContainer()
		{
			_container = new WindsorContainer()
				.Install(FromAssembly.This());

			GlobalConfiguration.Configuration.Services.Replace(typeof(IHttpControllerActivator),
				new HttpControllerActivator(_container));
			var controllerFactory = new WindsorControllerFactory(_container.Kernel);
			ControllerBuilder.Current.SetControllerFactory(controllerFactory);
		}

		protected void Application_End()
		{
			_container.Dispose();
		}
	}
}
