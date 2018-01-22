using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(BookEditorSPA.Startup))]

namespace BookEditorSPA
{
	public partial class Startup
	{
		public void Configuration(IAppBuilder app)
		{
		//	ConfigureAuth(app);
		}
	}
}
