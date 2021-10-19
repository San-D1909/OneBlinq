using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;

namespace Backend.Core.Logic
{
    public class DocInclusionPredicates
    {
        public static bool FilterByApiVersion(string version, ApiDescription desc)
        {
            if (!desc.TryGetMethodInfo(out var methodInfo))
                return false;

            var versions = methodInfo
               .DeclaringType?
           .GetCustomAttributes(true)
           .OfType<ApiVersionAttribute>()
           .SelectMany(attr => attr.Versions);

            var maps = methodInfo
               .GetCustomAttributes(true)
           .OfType<MapToApiVersionAttribute>()
           .SelectMany(attr => attr.Versions)
           .ToList();

            return versions?.Any(v => $"v{v}" == version) == true
                     && (!maps.Any() || maps.Any(v => $"v{v}" == version));
        }
    }
}
