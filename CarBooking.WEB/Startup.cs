using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CarBooking.WEB.Startup))]
namespace CarBooking.WEB
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
