using Microsoft.AspNetCore.Mvc;
using TrabalhoFinal.Domain.Entities;

namespace TrabalhoFinal.Controllers;

[ApiController]
[Route("[controller]")]
public class TodoController : ControllerBase
{
    private List<Todo> _todoList = new();
    private const int INITIAL_SIZE = 0;
    private int _count = INITIAL_SIZE;

    [HttpGet]
    [ProducesResponseType(typeof(List<Todo>), StatusCodes.Status200OK)]
    public IActionResult Get()
    {
        List<Todo> sortedTodos = _todoList
            .OrderBy(t => t.DueDateTime == null)
            .ToList();

        return Ok(sortedTodos);
    }


    [HttpPost]
    [ProducesResponseType(typeof(Todo), StatusCodes.Status200OK)]
    public IActionResult Create([FromBody] Todo newItem)
    {

        if (string.IsNullOrEmpty(newItem.Title))
        {
            return BadRequest("Title is required.");
        }

        if (newItem.Title.Length > 100)
        {
            return BadRequest("Title is too long.");
        }

        _count++;

        Todo? newTodo = new()
        {
            Id = _count,
            Description = newItem.Description,
            Title = newItem.Title,
            DueDateTime = newItem.DueDateTime
        };

        _todoList.Add(newTodo);

        return Ok(newTodo);
    }


    [HttpGet("{id}")]
    [ProducesResponseType(typeof(Todo), StatusCodes.Status200OK)]
    public IActionResult GetById(int id)
    {
        Todo? item = _todoList.Find(t => t.Id == id);

        if (item == null)
        {
            NotFound("Todo not found.");
        }

        return Ok(item);
    }


    [HttpPut("{id}")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    public IActionResult Update(int id, [FromBody] Todo updatedTodo)
    {
        Todo? item = _todoList.Find(t => t.Id == id);

        if (item == null)
        {
            NotFound("Todo not found.");
        }

        if (item != null)
        {
            item.Title = updatedTodo.Title;
            item.IsCompleted = updatedTodo.IsCompleted;
            item.DueDateTime = updatedTodo.DueDateTime;
        }

        return Ok("Todo updated successfuly.");
    }



    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    public IActionResult Delete(int id)
    {
        Todo? item = _todoList.Find(t => t.Id == id);

        if (item == null)
        {
            return NotFound();
        }

        _todoList.Remove(item);

        return Ok("Todo removed successfuly.");
    }

}
