using Yelload.Application.Common.Mappings;
using Yelload.Domain.Entities;

namespace Yelload.Application.TodoLists.Queries.ExportTodos;

public class TodoItemRecord : IMapFrom<TodoItem>
{
    public string? Title { get; set; }

    public bool Done { get; set; }
}
