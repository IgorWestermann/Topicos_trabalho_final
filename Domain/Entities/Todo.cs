namespace TrabalhoFinal.Domain.Entities
{
    public class Todo
    {
        public required int Id { get; set; }
        public required string Title { get; set; }
        public string? Description { get; set; }
        public bool? IsCompleted { get; set; }
        public DateTime? DueDateTime { get; set; }

        public Todo() { }

        public Todo(int id, string title, string? description, bool isCompleted, DateTime? dueDateTime)
        {
            Id = id;
            Title = title;
            Description = description;
            DueDateTime = dueDateTime;
            IsCompleted = false;
        }

        public void CheckTodo()
        {
            IsCompleted = true;
        }

    }
}
