using System;
using System.ComponentModel.DataAnnotations;

namespace ErasmusSystem.Entities
{
    public class User
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(100)]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [Required]
        [MaxLength(20)]
        public string Role { get; set; } // "Student" veya "Coordinator"

        public decimal? Gno { get; set; }

        public bool IsErasmusBefore { get; set; } = false;
    }
}