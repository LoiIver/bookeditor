using BookEditor.Data.Repositories;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace BookEditorSPA.Windsor
{
	public class Repositorynstaller : IWindsorInstaller
	{
		public void Install(IWindsorContainer container, IConfigurationStore store)
		{
			container.Register(Component.For<IBookRepository>().ImplementedBy<BookRepository>() 				
				.LifestyleSingleton());

		}
	}
}