using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RecipeMarket.Startup))]
namespace RecipeMarket
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
