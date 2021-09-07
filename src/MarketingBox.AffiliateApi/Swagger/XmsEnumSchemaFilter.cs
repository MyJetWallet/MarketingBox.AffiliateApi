using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace MarketingBox.AffiliateApi.Swagger
{
    internal class XmsEnumSchemaFilter : ISchemaFilter
    {
        private readonly XmsEnumExtensionsOptions _options;

        public XmsEnumSchemaFilter(XmsEnumExtensionsOptions options)
        {
            _options = options;
        }

        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            var type = context.Type;
            if (type.IsEnum)
            {
                XmsEnumExtensionApplicator.Apply(schema.Extensions, type, _options);
            };
        }
    }
}