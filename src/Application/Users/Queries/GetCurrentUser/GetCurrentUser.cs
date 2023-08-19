namespace AspNetCoreAngularTemplate.Application.Users.Queries.GetCurrentUser;

public record GetCurrentUserQuery : IRequest<string>;

public class GetCurrentUserQueryHandler : IRequestHandler<GetCurrentUserQuery, string>
{
    public Task<string> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
    {
        object? user = null;
        Guard.Against.NotFound("some id", user);
        return Task.FromResult("currentUser");
    }
}
