using Yelload.Application.TodoLists.Queries.ExportTodos;

namespace Yelload.Application.Common.Interfaces;

public interface ICsvFileBuilder
{
    byte[] BuildTodoItemsFile(IEnumerable<TodoItemRecord> records);
}
