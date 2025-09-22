using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProBook.Services.Database
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(100)]
        public string Surname { get; set; }

        [Required]
        [MaxLength(50)]
        public string Username { get; set; }

        [Required]
        [MaxLength(255)]
        public string Email { get; set; }

        public DateTime? RegisteredDate { get; set; } = DateTime.UtcNow;

        
        public string? PasswordHash { get; set; }

        
        public string? PasswordSalt { get; set; }

        public bool? IsStudent { get; set; }

        [MaxLength(20)]
        public string? TelephoneNumber { get; set; }

        [MaxLength(20)]
        public string? Gender { get; set; }

        public ICollection<Notebook> Notebooks { get; set; } = new List<Notebook>();
        public ICollection<Collection> Collections { get; set; }=new List<Collection>();
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public ICollection<SharedNotebook> SharedNotebooks { get; set; } = new List<SharedNotebook>();
    }
}
