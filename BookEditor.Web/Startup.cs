using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(BookEditor.Web.Startup))]

namespace BookEditor.Web
{
	public partial class Startup
	{
		public void Configuration(IAppBuilder app)
		{
		 
		}
	}
}
