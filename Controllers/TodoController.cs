using Microsoft.AspNetCore.Mvc;
using Todo_API.Models;
using Todo_API.Services;

namespace Todo_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TodoController : ControllerBase
    {
        public readonly ITodoService _todoService;
        
        public TodoController(ITodoService todoService)
        {
            _todoService = todoService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var sqlQuery = "SELECT * FROM c";
            var result = await _todoService.Get(sqlQuery);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Add(TaskModel task)
        {
            task.Id = Guid.NewGuid().ToString();
            var result = await _todoService.Add(task);

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update(TaskModel task)
        {
            var result = await _todoService.Update(task);

            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string id, string partition)
        {
            await _todoService.Delete(id, partition);

            return Ok();
        }
    }
}
