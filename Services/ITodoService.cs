using Todo_API.Models;

namespace Todo_API.Services
{
    public interface ITodoService
    {
        Task<List<TaskModel>> Get(string query);
        Task<TaskModel> Add(TaskModel task);
        Task<TaskModel> Update(TaskModel task);
        Task Delete(string id, string partition);
    }
}
