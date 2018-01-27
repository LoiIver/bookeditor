using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Mvc;
using BookEditor.Data.Contracts;
using BookEditor.Data.Repositories;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace BookEditor.Windsor
{
	public class Repositorynstaller : IWindsorInstaller
	{
		public void Install(IWindsorContainer container, IConfigurationStore store)
		{
			container.Register(Component.For<IBookRepository>().ImplementedBy<BookRepository>() 				
				.LifestyleSingleton());
			container.Register(Component.For<IAuthorRepository>().ImplementedBy<AuthorRepository>()
				.LifestyleSingleton());
			container.Register(Component.For<IPubHouseRepository>().ImplementedBy<PubHouseRepository>()
				.LifestyleSingleton());

		}
	}
}