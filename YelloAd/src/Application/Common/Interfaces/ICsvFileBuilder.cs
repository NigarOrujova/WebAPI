using YelloAd.Application.TodoLists.Queries.ExportTodos;

namespace YelloAd.Application.Common.Interfaces;

public interface ICsvFileBuilder
{
    byte[] BuildTodoItemsFile(IEnumerable<TodoItemRecord> records);
}
