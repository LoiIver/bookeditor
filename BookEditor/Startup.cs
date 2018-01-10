using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BookEditor.Startup))]
namespace BookEditor
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
