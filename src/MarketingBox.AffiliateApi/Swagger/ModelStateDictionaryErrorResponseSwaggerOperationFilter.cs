using System.Collections.Generic;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace MarketingBox.AffiliateApi.Swagger
{
    internal class ModelStateDictionaryErrorResponseSwaggerOperationFilter : IOperationFilter
    {
        private readonly int _statusCode;

        public ModelStateDictionaryErrorResponseSwaggerOperationFilter(int statusCode)
        {
            _statusCode = statusCode;
        }

        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            operation.Responses.Add(_statusCode.ToString(), new OpenApiResponse
            {
                Description = "Runtime error",
                Content = new Dictionary<string, OpenApiMediaType>
                {
                    ["application/json"] = new OpenApiMediaType
                    {
                        Example = new OpenApiObject
                        {
                            ["errors"] = new OpenApiObject{
                                ["property1"] = new OpenApiArray
                                {
                                    new OpenApiString("'property1' error description")
                                },
                                ["property2"] = new OpenApiArray
                                {
                                    new OpenApiString("'property2' error description")
                                },
                                ["property3"] = new OpenApiArray
                                {
                                    new OpenApiString("'property3' error description")
                                }
                            }
                        }
                    }
                }
            });
        }
    }
}