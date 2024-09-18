using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CustomChat.Application
{
    public static class ApplicationInjections
    {
        public static WebApplicationBuilder AddApplication(this WebApplicationBuilder e)
        {
            e.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            return e;
        }
    }
}
