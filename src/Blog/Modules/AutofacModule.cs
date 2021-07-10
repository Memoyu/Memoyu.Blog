using Autofac;
using Blog.Core.Security;
using Microsoft.AspNetCore.Http;

namespace Blog.Modules
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<HttpContextAccessor>().As<IHttpContextAccessor>().SingleInstance();
            builder.RegisterType<CurrentUser>().As<ICurrentUser>().InstancePerDependency();
        }
    }
}
