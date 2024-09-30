using Microsoft.AspNetCore.Mvc;
using TrabalhoFinal.Domain.Entities;
using TrabalhoFinal.Infra.Exceptions;

namespace TrabalhoFinal.Controllers;

[ApiController]
[Route("[controller]")]
public class TodoController : ControllerBase
{
    private readonly List<Todo> _todoList = new();
    private const int INITIAL_SIZE = 0;
    private int _count = INITIAL_SIZE;

    [HttpGet]
    [ProducesResponseType(typeof(List<Todo>), StatusCodes.Status200OK)]
    public List<Todo> Get()
    {
        List<Todo> sortedTodos = _todoList
            .OrderBy(t => t.DueDateTime == null)
            .ToList();

        return sortedTodos;
    }


    [HttpPost]
    [ProducesResponseType(typeof(Todo), StatusCodes.Status200OK)]
    public Todo Create([FromBody] Todo newItem)
    {

        if (string.IsNullOrEmpty(newItem.Title))
        {
            throw new ApiException("BadRequest", 404, "Title is required.");
        }

        if (newItem.Title.Length > 100)
        {
            throw new ApiException("BadRequest", 404, "Title is too long.");
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

        return newTodo;
    }


    [HttpGet("{id}")]
    [ProducesResponseType(typeof(Todo), StatusCodes.Status200OK)]
    public Todo GetById(int id)
    {
        Todo? item = _todoList.Find(t => t.Id == id);

        if (item == null)
        {
            throw new ApiException("NotFound", 404, "Todo not found.");
        }

        return item;
    }


    [HttpPut("{id}")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    public string Update(int id, [FromBody] Todo updatedTodo)
    {
        Todo? item = _todoList.Find(t => t.Id == id);

        if (item == null)
        {
            throw new ApiException("NotFound", 404, "Todo not found.");
        }

        item.Update(updatedTodo);

        return "Todo updated successfully.";
    }


    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    public string Delete(int id)
    {
        Todo? item = _todoList.Find(t => t.Id == id);

        if (item == null)
        {
            throw new ApiException("NotFound", 404, "Todo not found.");
        }

        _todoList.Remove(item);

        return "Todo removed successfuly.";
    }

}