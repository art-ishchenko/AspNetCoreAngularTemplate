namespace AspNetCoreAngularTemplate.Web.Infrastructure;

public static class IEndpointRouteBuilderExtensions
{
    public static IEndpointRouteBuilder MapGet(
        this IEndpointRouteBuilder builder,
        Delegate handler,
        string pattern = "",
        params object[] metadata)
    {
        return MapMethod(HttpMethods.Get, builder, handler, pattern, metadata);
    }

    public static IEndpointRouteBuilder MapPost(
        this IEndpointRouteBuilder builder,
        Delegate handler,
        string pattern = "",
        params object[] metadata)
    {
        return MapMethod(HttpMethods.Post, builder, handler, pattern, metadata);
    }

    public static IEndpointRouteBuilder MapPut(
        this IEndpointRouteBuilder builder,
        Delegate handler,
        string pattern,
        params object[] metadata)
    {
        return MapMethod(HttpMethods.Put, builder, handler, pattern, metadata);
    }

    public static IEndpointRouteBuilder MapDelete(
        this IEndpointRouteBuilder builder,
        Delegate handler,
        string pattern,
        params object[] metadata)
    {
        return MapMethod(HttpMethods.Delete, builder, handler, pattern, metadata);
    }
    
    private static IEndpointRouteBuilder MapMethod(
        string method,
        IEndpointRouteBuilder builder,
        Delegate handler,
        string pattern,
        object[] metadata)
    {
        Guard.Against.AnonymousMethod(handler);

        builder.MapMethods(pattern, new[] { method }, handler).WithName(handler.Method.Name).WithMetadata(metadata);

        return builder;
    }
}
