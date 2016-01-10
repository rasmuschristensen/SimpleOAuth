using System.Web.Http;
using Microsoft.Owin;
using Owin;
using SimpleAPI;

[assembly: OwinStartup(typeof(Startup))]
namespace SimpleAPI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {            
            var config = new HttpConfiguration();

            WebApiConfig.Register(config);
         
            ConfigureAuth(app);

            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.UseWebApi(config);
        }
    }
}