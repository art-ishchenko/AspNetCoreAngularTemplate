using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreAngularTemplate.Web.Infrastructure;

public static class ResponseTypes
{
    public static ProducesResponseTypeMetadata Accepted => new(202);
    public static ProducesResponseTypeMetadata Ok() => new(200);
    public static ProducesResponseTypeMetadata Ok<T>() => new(200, typeof(T));
    
    public static readonly ProducesResponseTypeMetadata BadRequest = new(400, typeof(ValidationProblemDetails));
    
    public static readonly ProducesResponseTypeMetadata NotFound = new(404, typeof(ProblemDetails));
    
    public static readonly ProducesResponseTypeMetadata Forbidden = new(403, typeof(ProblemDetails));
}
