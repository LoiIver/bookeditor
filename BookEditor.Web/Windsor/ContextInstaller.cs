using BookEditor.Data.Contracts;
using BookEditor.Data.Repositories;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace BookEditor.Web.Windsor
{
	public class ContextInstaller : IWindsorInstaller
	{
		public void Install(IWindsorContainer container, IConfigurationStore store)
		{
			container.Register(Component.For<IDataContext>().ImplementedBy<DataContext>()
					.LifestyleSingleton());
			container.Register(Component.For<IBookRepository>().ImplementedBy<BookRepository>()
				.LifestyleSingleton());
			container.Register(Component.For<IBookAuthorRepository>().ImplementedBy<BookAuthorRepository>()
				.LifestyleSingleton());
			container.Register(Component.For<IAuthorRepository>().ImplementedBy<AuthorRepository>()
				.LifestyleSingleton());
			container.Register(Component.For<IPubHouseRepository>().ImplementedBy<PubHouseRepository>()
				.LifestyleSingleton());

		}
	}
}