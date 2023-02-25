using Microsoft.AspNetCore.Mvc;

namespace Studentby.Pres.WebApi.Atributes;

internal class ApiRouteAttribute : RouteAttribute
{
    public ApiRouteAttribute(string template) : base($"api/{template}") { }
    public ApiRouteAttribute() : base("api") { }
}
