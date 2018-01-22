using System.Web.Http.Controllers;
using System.Web.Mvc;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace BookEditorSPA.Windsor
{
	public class ControllerInstaller : IWindsorInstaller
	{
		public void Install(IWindsorContainer container, IConfigurationStore store)
		{
			container.Register(Classes.FromThisAssembly()
				.BasedOn<IController>()
				.LifestyleTransient());
			container.Register(Classes.FromThisAssembly()
			   .BasedOn<IHttpController>()
			   .LifestyleTransient());
		}
	}
}