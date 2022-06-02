using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TruongNgocThanh_BigSchool.Startup))]
namespace TruongNgocThanh_BigSchool
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
