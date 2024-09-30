using TrabalhoFinal.Controllers;
using TrabalhoFinal.Domain.Entities;
using TrabalhoFinal.Infra.Exceptions;

namespace TrabalhoFinal.UnitTests;

public class TodoControllerTests
{
    private TodoController _controller;
    public TodoControllerTests()
    {
        _controller = new TodoController();
    }

    [Fact]
    public void ShouldReturnBadRequest_WhenTitleIsEmpty()
    {
        // Arrange
        Todo newItem = new()
        {
            Id = 1,
            Title = string.Empty,
            Description = "Description"
        };

        // Act
        // Assert
        var exception = Assert.Throws<ApiException>(() => _controller.Create(newItem));
        Assert.Equal("BadRequest", exception.Type);
        Assert.Equal(404, exception.StatusCode);
        Assert.Equal("Title is required.", exception.Message);
    }

    [Fact]
    public void ShouldReturnBadRequest_WhenTitleIsTooLong()
    {
        // Arrange
        Todo newItem = new()
        {
            Id = 1,
            Title = new string('a', 101),
            Description = "Description"
        };

        // Act
        // Assert
        var exception = Assert.Throws<ApiException>(() => _controller.Create(newItem));
        Assert.Equal("BadRequest", exception.Type);
        Assert.Equal(404, exception.StatusCode);
        Assert.Equal("Title is too long.", exception.Message);
    }

    [Fact]
    public void ShouldReturnTodo_WhenCreatingNewTodoWithValidTitle()
    {
        // Arrange
        Todo newItem = new()
        {
            Id = 1,
            Title = "Valid Title",
            Description = "Description"
        };

        // Act
        var result = _controller.Create(newItem);

        // Assert
        Assert.IsType<Todo>(result);
        Assert.Equal("Valid Title", result.Title);
    }

    [Fact]
    public void ShouldReturnTodo_WhenGettingById()
    {
        // Arrange
        Todo newItem = new()
        {
            Id = 1,
            Title = "Todo 1",
            Description = "Description 1"
        };
        _controller.Create(newItem);

        // Act
        var result = _controller.GetById(1);

        // Assert
        Assert.IsType<Todo>(result);
        Assert.Equal("Todo 1", result.Title);
    }

    [Fact]
    public void ShouldReturnNotFound_WhenGettingByIdDoesNotExist()
    {
        // Act
        // Assert
        var exception = Assert.Throws<ApiException>(() => _controller.GetById(99));
        Assert.Equal("NotFound", exception.Type);
        Assert.Equal(404, exception.StatusCode);
        Assert.Equal("Todo not found.", exception.Message);
    }

    [Fact]
    public void ShouldReturnUpdatedMessage_WhenUpdatingTodo()
    {
        // Arrange
        Todo newItem = new()
        {
            Id = 1,
            Title = "Todo to Update",
            Description = "Description"
        };
        _controller.Create(newItem);

        Todo updatedTodo = new()
        {
            Id = 1,
            Title = "Updated Todo",
            Description = "Updated Description"
        };

        // Act
        var result = _controller.Update(1, updatedTodo);

        // Assert
        Assert.Equal("Todo updated successfully.", result);
    }

    [Fact]
    public void ShouldReturnNotFound_WhenUpdatingTodoDoesNotExist()
    {
        // Arrange
        Todo updatedTodo = new()
        {
            Id = 1,
            Title = "Updated Todo",
            Description = "Updated Description"
        };

        // Act
        // Assert
        var exception = Assert.Throws<ApiException>(() => _controller.Update(99, updatedTodo));
        Assert.Equal("NotFound", exception.Type);
        Assert.Equal(404, exception.StatusCode);
        Assert.Equal("Todo not found.", exception.Message);
    }

    [Fact]
    public void ShouldReturnDeletedMessage_WhenDeletingTodo()
    {
        // Arrange
        Todo newItem = new()
        {
            Id = 1,
            Title = "Todo to Delete",
            Description = "Description"
        };
        _controller.Create(newItem);

        // Act
        var result = _controller.Delete(1);

        // Assert
        Assert.Equal("Todo removed successfuly.", result);
    }

    [Fact]
    public void ShouldReturnNotFound_WhenDeletingTodoDoesNotExist()
    {
        // Act
        // Assert
        var exception = Assert.Throws<ApiException>(() => _controller.Delete(99));
        Assert.Equal("NotFound", exception.Type);
        Assert.Equal(404, exception.StatusCode);
        Assert.Equal("Todo not found.", exception.Message);
    }
}
