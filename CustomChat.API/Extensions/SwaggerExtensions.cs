using Microsoft.OpenApi.Models;

namespace CustomChat.API.Extensions
{
    public static class SwaggerExtensions
    {
        public static IEndpointConventionBuilder WithCustomGroup(this IEndpointConventionBuilder builder, string groupName)
        {
            builder.WithOpenApi(options =>
            {
                options.Tags[0] = new OpenApiTag() { Name = groupName };
                return options;
            });

            return builder;
        }

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
