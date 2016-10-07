using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BookCoffee.Startup))]
namespace BookCoffee
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
