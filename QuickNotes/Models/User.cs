using System.ComponentModel.DataAnnotations;

namespace QuickNotes.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string Username { get; set; } = null!;

        [Required]
        public byte[] PasswordHash { get; set; } = null!;

        [Required]
        public byte[] PasswordSalt { get; set; } = null!;

        public ICollection<Note> Notes { get; set; } = new List<Note>();
    }
}
