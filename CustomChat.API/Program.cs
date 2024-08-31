using Azure.AI.OpenAI;
using CustomChat.API.Extensions;
using CustomChat.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace CustomChat.API
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.AddSwaggerAuthorize();

            builder.AddRepositories().AddDbContext().AddIdentity();

            builder.Services.AddMediatR(cfg => 
            cfg.RegisterServicesFromAssembly(Assembly.Load(
                AppDomain.CurrentDomain.GetAssemblies().SingleOrDefault(x => x.FullName!.Contains(nameof(Application)))!.GetName()               
            )));
            builder.Services.AddScoped(x => new OpenAIClient(Environment.GetEnvironmentVariable("OPENAI_API_KEY")));

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors(builder =>
            {
                builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
            });


            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapGroup("/auth")
                .MapIdentityApi<IdentityUser>()
                .WithCustomGroup("Authentication");

            app.MapControllers();

            app.Run();
        }
    }
    public static class ProgramExtensions
    {
        public static WebApplicationBuilder? AddSwaggerAuthorize(this WebApplicationBuilder? e)
        {
            e.Services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
                option.AddSecurityDefinition(
                    "Bearer",
                    new OpenApiSecurityScheme
                    {
                        In = ParameterLocation.Header,
                        Description = "Please enter a valid token",
                        Name = "Authorization",
                        Type = SecuritySchemeType.Http,
                        BearerFormat = "JWT",
                        Scheme = "Bearer"
                    });
                option.AddSecurityRequirement(
                    new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },new string[] { }
                        }
                    });
            });

            return e;
        }
    }

}
