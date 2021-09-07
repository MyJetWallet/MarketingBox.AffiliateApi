using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace MarketingBox.AffiliateApi.Swagger
{
    internal class AuthorizationCheckSwaggerOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var requiredScopes = context.MethodInfo
                .GetCustomAttributes(true)
                .OfType<AuthorizeAttribute>()
                .Select(attr => attr.Policy);

            if (context.MethodInfo.DeclaringType != null)
            {
                var requiredScopesOnController = context.MethodInfo.DeclaringType
                    .GetCustomAttributes(true)
                    .OfType<AuthorizeAttribute>()
                    .Select(attr => attr.Policy);

                requiredScopes = requiredScopes.Concat(requiredScopesOnController).Distinct();
            }
            

            if (requiredScopes.Any())
            {
                operation.Responses.Add("401", new OpenApiResponse { Description = "Unauthorized" });
                operation.Responses.Add("403", new OpenApiResponse { Description = "Forbidden" });

                operation.Security = new List<OpenApiSecurityRequirement>
                {
                    new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference {Type = ReferenceType.SecurityScheme, Id = "Bearer"},
                                Scheme = "oauth2",
                                Name = "Bearer",
                                In = ParameterLocation.Header,

                            },
                            new List<string>()
                        }
                    }
                };
            }
        }
    }
}