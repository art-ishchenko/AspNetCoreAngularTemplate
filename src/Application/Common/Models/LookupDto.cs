using AspNetCoreAngularTemplate.Domain.Entities;

namespace AspNetCoreAngularTemplate.Application.Common.Models;

public class LookupDto
{
    public string? Id { get; init; }

    public string? Title { get; init; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<TodoList, LookupDto>();
            CreateMap<TodoItem, LookupDto>();
        }
    }
}
