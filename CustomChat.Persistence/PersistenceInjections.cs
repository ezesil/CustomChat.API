using CustomChat.Persistence.Context;
using CustomChat.Persistence.PersistenceServices;
using CustomChat.Persistence.Repositories;
using CustomChat.Domain.Interfaces.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CustomChat.Persistence
{
    public static class PersistenceInjections
    {
        public static WebApplicationBuilder AddDbContext(this WebApplicationBuilder e)
        {
            e.Services.AddDbContext<ChatDbContext>(x =>
                x.UseNpgsql(Environment.GetEnvironmentVariable("CRDB_CONN_STRING"),
                b => b.MigrationsAssembly(Assembly.GetCallingAssembly().FullName)
            ));

            e.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            return e;
        }

        public static WebApplicationBuilder AddRepositories(this WebApplicationBuilder e)
        {
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();

            Type[] repositories = assemblies.SelectMany(s => s.GetTypes())
                .Where(p => p.IsClass && !p.IsAbstract
                && p.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IRepository<>) && p.Name != "Repository`1"))
                .ToArray();

            foreach (Type repositoryType in repositories)
            {
                Type? @interface = repositoryType.GetInterface($"I{repositoryType.Name}", false);

                if (@interface != null)
                {
                    e.Services.AddTransient(@interface, repositoryType);
                }
            }

            return e;
        }

        public static WebApplicationBuilder AddIdentity(this WebApplicationBuilder e)
        {
            e.Services.AddSingleton(TimeProvider.System);
            e.Services.AddSingleton<IEmailSender<IdentityUser>, EmailSender>();

            e.Services.AddDbContext<IdentityContext>(options =>
                options.UseNpgsql(Environment.GetEnvironmentVariable("CRDB_CONN_STRING"),
                b => b.MigrationsAssembly(Assembly.GetCallingAssembly().GetName().Name)
            ));

            e.Services.AddIdentityApiEndpoints<IdentityUser>()
                .AddEntityFrameworkStores<IdentityContext>();

            e.Services.Configure<IdentityOptions>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;
            });

            return e;
        }
    }
}
