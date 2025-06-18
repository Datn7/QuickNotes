namespace QuickNotes.Models
{
    public class Tag
    {
        public int Id { get; set; }
        public string Label { get; set; } = null!;

        public ICollection<Note> Notes { get; set; } = new List<Note>();
    }
}
