namespace QuickNotes.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public ICollection<Note> Notes { get; set; } = new List<Note>();
    }
}
