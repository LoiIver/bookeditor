using System.Web.Http;
using Newtonsoft.Json.Serialization;

namespace BookEditor
{
	public static class WebApiConfig
	{
		public static void Register(HttpConfiguration config)
		{
			//Att
			config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
			config.MapHttpAttributeRoutes();
			config.Routes.MapHttpRoute(
				name: "DefaultApi",
				routeTemplate: "api/{controller}/{id}",
				defaults: new { id = RouteParameter.Optional }
			);
		}
	}
}
