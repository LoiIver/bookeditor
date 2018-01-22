using System;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using Castle.Windsor;

namespace BookEditorSPA.Windsor
{

	internal class HttpControllerActivator : IHttpControllerActivator
	{
		private readonly IWindsorContainer _container;

		public HttpControllerActivator(IWindsorContainer container)
		{
			_container = container;
		}

		public IHttpController Create(
		  HttpRequestMessage request,
		  HttpControllerDescriptor controllerDescriptor,
		  Type controllerType)
		{
			var controller =
			  (IHttpController)_container.Resolve(controllerType);

			request.RegisterForDispose(
			  new Release(
				() => this._container.Release(controller)));

			return controller;
		}

		private class Release : IDisposable
		{
			private readonly Action _release;

			public Release(Action release)
			{
				this._release = release;
			}

			public void Dispose()
			{
				this._release();
			}
		}
	}
}