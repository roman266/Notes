namespace Notes.Models
{
    public class NoteModel
    {
        public string UserId { get; set; }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Priority Priority { get; set; }
        public DateTime CreateDate { get; set; }
    }

    public enum Priority
    {
        Low,
        Medium,
        High
    }
}
