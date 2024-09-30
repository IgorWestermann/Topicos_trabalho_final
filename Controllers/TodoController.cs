using Microsoft.AspNetCore.Mvc;
using TrabalhoFinal.Domain.Entities;

namespace TrabalhoFinal.Controllers;

[ApiController]
[Route("[controller]")]
public class TodoController : ControllerBase
{
    private static List<Todo> _todoList = new();
    private static int _count = 0; // Code Smell: Magic numbers

    // Code Smell:
    // - Large method that does too many things (S.O.L.I.D)
    // - Inefficient use  of LINQ
    [HttpGet]
    public IActionResult Get()
    {
        var sortedTodos = _todoList
            .OrderBy(t => t.DueDateTime == null)
            .ThenBy(t => t.DueDateTime)
            .ToList();

        return Ok(sortedTodos);
    }


    // Code Smells:
    // - Method that has hardcoded responses and no validation
    // - Global mutable state
    // - Hardcoded rule for item creation
    // - Magic number
    // Potential Bug:
    // - Inconsistent response
    // -No validation for Title
    [HttpPost]
    public IActionResult Create(Todo newItem)
    {
        _count++;
        newItem.Id = _count;

        if (string.IsNullOrEmpty(newItem.Title))
        {
            return BadRequest("Title is required.");
        }


        if (newItem.Title.Length > 100)
        {
            return BadRequest("Title is too long.");
        }

        _todoList.Add(newItem);


        return CreatedAtAction(nameof(GetById), new { id = newItem.Id }, newItem);
    }

    // Code Smells:
    // - Duplicate code for null check
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var item = _todoList.FirstOrDefault(t => t.Id == id);

        if (item == null)
        {
            return NotFound();
        }

        return Ok(item);
    }

    // Code Smells:
    // - Duplicate code for null check
    // - No validation on update
    // - Inefficient and verbose delete operation
    [HttpPut("{id}")]
    public IActionResult Update(int id, Todo updatedTodo)
    {
        var item = _todoList.FirstOrDefault(t => t.Id == id);


        if (item == null)
        {
            return NotFound();
        }


        item.Title = updatedTodo.Title;
        item.IsCompleted = updatedTodo.IsCompleted;
        item.DueDateTime = updatedTodo.DueDateTime;

        return NoContent();
    }


    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var item = _todoList.FirstOrDefault(t => t.Id == id);

        if (item == null)
        {
            return NotFound();
        }

        _todoList.Remove(item);
        return NoContent();
    }

}
