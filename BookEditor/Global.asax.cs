using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using BookEditor.Windsor;
using Castle.Windsor;
using Castle.Windsor.Installer;

namespace BookEditor
{
	public class MvcApplication : System.Web.HttpApplication
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

			//var controllerFactory = new WindsorControllerFactory(_container.Kernel);
			GlobalConfiguration.Configuration.Services.Replace(typeof(IHttpControllerActivator),
				new HttpControllerActivator(_container));

			//ControllerBuilder.Current.SetControllerFactory(controllerFactory);
		}

		protected void Application_End()
		{
			_container.Dispose();
		}
	}
}
