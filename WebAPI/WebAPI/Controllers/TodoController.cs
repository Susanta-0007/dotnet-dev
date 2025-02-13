using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/todos")]
    public class TodoController : ControllerBase
    {
        private static List<TodoItem> todos = new List<TodoItem>
        {
            new TodoItem { Id = 1, Title = "Learn .NET Core", IsCompleted = false },
            new TodoItem { Id = 2, Title = "Build a REST API", IsCompleted = true },
            new TodoItem { Id = 3, Title = "Test the API using Swagger", IsCompleted = false }
        };
        private static int nextId = 4; // Set the next ID based on the last task

        // GET: api/todos (Get all tasks)
        [HttpGet]
        public IActionResult GetTodos()
        {
            return Ok(todos);
        }

        // GET: api/todos/{id} (Get a single task by ID)
        [HttpGet("{id}")]
        public IActionResult GetTodoById(int id)
        {
            var todo = todos.FirstOrDefault(t => t.Id == id);
            if (todo == null)
                return NotFound();

            return Ok(todo);
        }

        // POST: api/todos (Create a new task)
        [HttpPost]
        public IActionResult CreateTodo([FromBody] TodoItem todo)
        {
            if (string.IsNullOrWhiteSpace(todo.Title))
                return BadRequest("Title is required.");

            todo.Id = nextId++;
            todos.Add(todo);
            return CreatedAtAction(nameof(GetTodoById), new { id = todo.Id }, todo);
        }

        // PUT: api/todos/{id} (Update a task)
        [HttpPut("{id}")]
        public IActionResult UpdateTodo(int id, [FromBody] TodoItem updatedTodo)
        {
            var todo = todos.FirstOrDefault(t => t.Id == id);
            if (todo == null)
                return NotFound();

            todo.Title = updatedTodo.Title;
            todo.IsCompleted = updatedTodo.IsCompleted;
            return Ok(todo);
        }

        // DELETE: api/todos/{id} (Delete a task)
        [HttpDelete("{id}")]
        public IActionResult DeleteTodo(int id)
        {
            var todo = todos.FirstOrDefault(t => t.Id == id);
            if (todo == null)
                return NotFound();

            todos.Remove(todo);
            return NoContent();
        }
    }
}
