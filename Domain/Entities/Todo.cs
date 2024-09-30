namespace TrabalhoFinal.Domain.Entities
{
    public class Todo
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime? DueDateTime { get; set; }

        public void CheckTodo()
        {
            IsCompleted = true;
        }
    }
}
