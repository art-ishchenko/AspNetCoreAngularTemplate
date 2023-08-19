using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreAngularTemplate.Web.Infrastructure;

public static class ResponseTypes
{
    public static ProducesResponseTypeMetadata Ok<T>() => new(200, typeof(T));
    
    public static readonly ProducesResponseTypeMetadata NotFound = new(404, typeof(ProblemDetails));
}
