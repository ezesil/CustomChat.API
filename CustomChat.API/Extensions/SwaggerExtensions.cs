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
    }
}
