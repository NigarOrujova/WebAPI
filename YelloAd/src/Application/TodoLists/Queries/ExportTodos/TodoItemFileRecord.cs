using YelloAd.Application.Common.Mappings;
using YelloAd.Domain.Entities;

namespace YelloAd.Application.TodoLists.Queries.ExportTodos;

public class TodoItemRecord : IMapFrom<TodoItem>
{
    public string? Title { get; set; }

    public bool Done { get; set; }
}
